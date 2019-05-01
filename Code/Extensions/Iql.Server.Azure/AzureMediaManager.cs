using System;
using System.Globalization;
using System.Threading.Tasks;
using Iql.Entities.PropertyGroups.Files;
using Iql.Server.Media;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Iql.Server.Azure
{
    public class AzureMediaManager : MediaManager
    {
        public IAzureConnectionDetails ConnectionDetails { get; }

        public AzureMediaManager(IAzureConnectionDetails connectionDetails)
        {
            ConnectionDetails = connectionDetails;
        }

        protected async Task<CloudBlockBlob> GetBlobAsync(string container, string id, bool createIfNotExists = true)
        {
            var containerReference = await GetContainerAsync(container, createIfNotExists);
            var blockBlob = containerReference.GetBlockBlobReference(id);
            return blockBlob;
        }

        protected async Task<CloudBlobContainer> GetContainerAsync(string name, bool createIfNotExists = true)
        {
            var client = NewClient();
            // Retrieve a reference to a container.
            var container = client.GetContainerReference(AzureSafe(name));
            if (createIfNotExists)
            {
                await container.CreateIfNotExistsAsync();
            }
            return container;
        }

        private CloudBlobClient NewClient()
        {
            var storageAccount = CloudStorageAccount.Parse(
                ConnectionDetails.ConnectionString);

            // Create the blob client.
            var blobClient = storageAccount.CreateCloudBlobClient();
            return blobClient;
        }

        private string AzureSafe(string name)
        {
            return name;
        }

        public override async Task<string> SetMediaUriAsync<T>(
            T entity,
            IFileUrl<T> file,
            MediaAccessKind accessKind,
            TimeSpan? lifetime = null)
        {
            if (file.MediaKey == null)
            {
                throw new ArgumentException("Must have a MediaKey to get a MediaUri");
            }

            if (file.MediaKey.Groups.Count != 2)
            {
                throw new ArgumentException("Azure MediaKey must consist of two groups");
            }

            // TODO: Should use preview or file
            var existingReadUrl = file.UrlProperty.GetValue(entity) as string;
            if (!string.IsNullOrWhiteSpace(existingReadUrl))
            {
                var existingUri = new Uri(new Uri(existingReadUrl).GetLeftPart(UriPartial.Path));
                var expectedBlob = await BuildCloudBlockBlobAsync(entity, file, false);
                if (existingUri.GetLeftPart(UriPartial.Path).ToLower() != expectedBlob.Uri.GetLeftPart(UriPartial.Path).ToLower())
                {
                    file.UrlProperty.SetValue(entity, null);
                    existingReadUrl = null;
                }
            }
            var cloudBlockBlob = await GetCloudBlockBlob(entity, file);
            var sasToken = cloudBlockBlob.GetSharedAccessSignature(
                accessKind == MediaAccessKind.Admin ? AdminPolicy(lifetime) : ReadOnlyPolicy(lifetime));
            if (string.IsNullOrWhiteSpace(existingReadUrl))
            {
                var readOnlySasToken = cloudBlockBlob.GetSharedAccessSignature(ReadOnlyPolicy());
                var readOnlyUrl = string.Format(CultureInfo.InvariantCulture, "{0}{1}", cloudBlockBlob.Uri, readOnlySasToken);
                file.UrlProperty.SetValue(entity, readOnlyUrl);
            }
            var url = string.Format(CultureInfo.InvariantCulture, "{0}{1}", cloudBlockBlob.Uri, sasToken);
            return url;
        }

        private async Task<CloudBlockBlob> GetCloudBlockBlob<T>(T entity, IFileUrl<T> file, bool createIfNotExists = true)
            where T : class
        {
            var existingUrl = file.UrlProperty.GetValue(entity) as string;
            string containerName = null;
            string id = null;
            if (!string.IsNullOrWhiteSpace(existingUrl))
            {
                if (Uri.TryCreate(existingUrl, UriKind.Absolute, out var uri))
                {
                    var cloudBlockBlob = new CloudBlockBlob(uri);
                    containerName = cloudBlockBlob.Container.Name;
                    id = cloudBlockBlob.Name;
                }
            }

            if (string.IsNullOrWhiteSpace(containerName))
            {
                return await BuildCloudBlockBlobAsync(entity, file, createIfNotExists);
            }

            return await GetBlobAsync(containerName, id, createIfNotExists);
        }

        protected async Task<CloudBlockBlob> BuildCloudBlockBlobAsync<T>(T entity, IFileUrl<T> file, bool createIfNotExists)
            where T : class
        {
            var containerName = file.MediaKey.Groups[0].EvaluateToString(entity);
            var id = file.MediaKey.Groups[1].EvaluateToString(entity);

            CloudBlockBlob cloudBlockBlob = null;
            if (!string.IsNullOrWhiteSpace(containerName))
            {
                cloudBlockBlob = await GetBlobAsync(containerName, id, createIfNotExists);
            }
            return cloudBlockBlob;

        }

        public static SharedAccessBlobPolicy AdminPolicy(TimeSpan? lifetime = null)
        {
            return SetLifeTime(new SharedAccessBlobPolicy
            {
                Permissions = SharedAccessBlobPermissions.Read |
                              SharedAccessBlobPermissions.Create |
                              SharedAccessBlobPermissions.Add |
                              SharedAccessBlobPermissions.Write |
                              SharedAccessBlobPermissions.Delete |
                              SharedAccessBlobPermissions.List
            }, lifetime);
        }

        public static SharedAccessBlobPolicy ReadOnlyPolicy(TimeSpan? lifetime = null)
        {
            return SetLifeTime(new SharedAccessBlobPolicy
            {
                Permissions = SharedAccessBlobPermissions.Read
            }, lifetime);
        }

        protected static SharedAccessBlobPolicy SetLifeTime(SharedAccessBlobPolicy policy, TimeSpan? lifetime)
        {
            policy.SharedAccessStartTime = DateTimeOffset.UtcNow;
            policy.SharedAccessExpiryTime = lifetime.HasValue
                ? DateTime.UtcNow.Add(lifetime.Value)
                : DateTimeOffset.MaxValue;
            return policy;
        }

        public override async Task CloneAsync<T>(T fromEntity, T toEntity, IFileUrl<T> file)
        {
            var fromBlob = await GetCloudBlockBlob(fromEntity, file, false);
            if (fromBlob != null && await fromBlob.ExistsAsync())
            {
                var toBlob = await GetCloudBlockBlob(toEntity, file);
                await fromBlob.StartCopyAsync(toBlob);
            }
        }

        public override async Task CloneUrlAsync(string fromUrl, string toUrl)
        {
            var client = NewClient();
            var fromBlob = new CloudBlockBlob(new Uri(new Uri(fromUrl).GetLeftPart(UriPartial.Path)), client);
            var toBlob = new CloudBlockBlob(new Uri(new Uri(toUrl).GetLeftPart(UriPartial.Path)), client);
            if (await fromBlob.ExistsAsync())
            {
                await toBlob.Container.CreateIfNotExistsAsync();
                await toBlob.StartCopyAsync(fromBlob);
            }
        }

        public override Task<Func<Task>> GetDeleteTaskAsync<T>(T entity, IFileUrl<T> file)
        {
            Func<Task> task = async () =>
            {
                // Should use file or preview
                var blob = await GetCloudBlockBlob(entity, file, false);
                if (blob != null)
                {
                    await blob.DeleteIfExistsAsync();
                }
            };
            return Task.FromResult(task);
        }
    }
}