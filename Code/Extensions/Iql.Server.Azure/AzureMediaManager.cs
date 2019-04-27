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

        protected async Task<CloudBlockBlob> GetBlobAsync(string mycontainer, string id)
        {
            var container = await GetContainerAsync(mycontainer);

            var blockBlob = container.GetBlockBlobReference(id);

            return blockBlob;
        }

        protected async Task<CloudBlobContainer> GetContainerAsync(string name)
        {
            var storageAccount = CloudStorageAccount.Parse(
                ConnectionDetails.ConnectionString);

            // Create the blob client.
            var blobClient = storageAccount.CreateCloudBlobClient();
            
            // Retrieve a reference to a container.
            var container = blobClient.GetContainerReference(AzureSafe(name));
            await container.CreateIfNotExistsAsync();
            return container;
        }

        private string AzureSafe(string name)
        {
            return name;
        }

        public override async Task<string> GetMediaUriAsync<T>(T entity, IFileUrl<T> file, MediaAccessKind accessKind,
            TimeSpan? lifetime = null)
        {
            if (file.MediaKey == null)
            {
                throw new ArgumentException("Must have a MediaKey to get a MediaUri");
            }

            if (file.MediaKey.Groups.Count != 2)
            {
                throw new ArgumentException("Azure MediaKey must consiste of two groups");
            }

            // TODO: Should use preview or file
            var cloudBlockBlob = await GetCloudBlockBlob(entity, file);
            var sasToken = cloudBlockBlob.GetSharedAccessSignature(
                accessKind == MediaAccessKind.Admin ? AdminPolicy(lifetime) : ReadOnlyPolicy(lifetime));
            var existingReadUrl = file.UrlProperty.GetValue(entity) as string;
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
            CloudBlockBlob cloudBlockBlob = null;
            var existingUrl = file.UrlProperty.GetValue(entity) as string;
            string containerName = null;
            string id = null;
            if (!string.IsNullOrWhiteSpace(existingUrl))
            {
                if (Uri.TryCreate(existingUrl, UriKind.Absolute, out var uri))
                {
                    cloudBlockBlob = new CloudBlockBlob(uri);
                    containerName = cloudBlockBlob.Container.Name;
                    id = cloudBlockBlob.Name;
                }
            }

            if (string.IsNullOrWhiteSpace(containerName) && createIfNotExists)
            {
                containerName = file.MediaKey.Groups[0].EvaluateToString(entity);
                id = file.MediaKey.Groups[1].EvaluateToString(entity);
            }

            if (!string.IsNullOrWhiteSpace(containerName))
            {
                cloudBlockBlob = await GetBlobAsync(containerName, id);
                if (createIfNotExists)
                {
                    await cloudBlockBlob.Container.CreateIfNotExistsAsync();
                }
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

        public override  Task<Func<Task>> GetDeleteTaskAsync<T>(T entity, IFileUrl<T> file)
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