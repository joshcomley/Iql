using System;
using System.Globalization;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Azure.Storage.Sas;
using Iql.Entities.PropertyGroups.Files;
using Iql.Server.Media;

namespace Iql.Server.Azure
{
    public class AzureMediaManager : MediaManager
    {
        public IAzureConnectionDetails ConnectionDetails { get; }

        public AzureMediaManager(IAzureConnectionDetails connectionDetails)
        {
            ConnectionDetails = connectionDetails;
        }

        protected async Task<BlobClient> GetBlobAsync(string container, string id, bool createIfNotExists = true)
        {
            var containerReference = await GetContainerAsync(container, createIfNotExists);
            var blockBlob = containerReference.GetBlobClient(id);
            return blockBlob;
        }

        protected async Task<BlobContainerClient> GetContainerAsync(string name, bool createIfNotExists = true)
        {
            var client = NewClient();
            // Retrieve a reference to a container.
            var container = client.GetBlobContainerClient(AzureSafe(name));
            if (createIfNotExists && !await container.ExistsAsync())
            {
                var result = await container.CreateIfNotExistsAsync();
                if (result.HasValue)
                {
                    await container.SetAccessPolicyAsync(PublicAccessType.Blob);
                }
            }

            return container;
        }

        private BlobServiceClient NewClient()
        {
            var storageAccount = new BlobServiceClient(ConnectionDetails.ConnectionString);
            return storageAccount;
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
            var mediaKey = file.MediaKey ?? file.RootFile?.MediaKey;
            if (mediaKey == null)
            {
                throw new ArgumentException("Must have a MediaKey to get a MediaUri");
            }

            if (mediaKey.Groups.Count != 2)
            {
                throw new ArgumentException("Azure MediaKey must consist of two groups");
            }

            // TODO: Should use preview or file
            var existingReadUrl = file.UrlProperty.GetValue(entity) as string;
            if (!string.IsNullOrWhiteSpace(existingReadUrl))
            {
                var existingUri = new Uri(new Uri(existingReadUrl).GetLeftPart(UriPartial.Path));
                var expectedBlob = await BuildCloudBlockBlobAsync(entity, file, false);
                if (existingUri.GetLeftPart(UriPartial.Path).ToLower() !=
                    expectedBlob.Uri.GetLeftPart(UriPartial.Path).ToLower())
                {
                    file.UrlProperty.SetValue(entity, null);
                    existingReadUrl = null;
                }
            }

            var cloudBlockBlob = await GetCloudBlockBlob(entity, file);
            var sasToken = cloudBlockBlob.GenerateSasUri(
                accessKind == MediaAccessKind.Admin ? AdminPolicy(lifetime) : ReadOnlyPolicy(lifetime));
            if (string.IsNullOrWhiteSpace(existingReadUrl))
            {
                var readOnlySasToken = cloudBlockBlob.GenerateSasUri(ReadOnlyPolicy());
                var readOnlyUrl = string.Format(CultureInfo.InvariantCulture, "{0}{1}", cloudBlockBlob.Uri,
                    readOnlySasToken);
                file.UrlProperty.SetValue(entity, readOnlyUrl);
            }

            var url = string.Format(CultureInfo.InvariantCulture, "{0}{1}", cloudBlockBlob.Uri, sasToken);
            return url;
        }

        private async Task<BlobClient> GetCloudBlockBlob<T>(T entity, IFileUrl<T> file, bool createIfNotExists = true)
            where T : class
        {
            var existingUrl = file.UrlProperty.GetValue(entity) as string;
            string containerName = null;
            string id = null;
            if (!string.IsNullOrWhiteSpace(existingUrl))
            {
                if (Uri.TryCreate(existingUrl, UriKind.Absolute, out var uri))
                {
                    var cloudBlockBlob = new BlobClient(uri);
                    containerName = cloudBlockBlob.BlobContainerName;
                    id = cloudBlockBlob.Name;
                }
            }

            if (string.IsNullOrWhiteSpace(containerName))
            {
                return await BuildCloudBlockBlobAsync(entity, file, createIfNotExists);
            }

            return await GetBlobAsync(containerName, id, createIfNotExists);
        }

        protected async Task<BlobClient> BuildCloudBlockBlobAsync<T>(T entity, IFileUrl<T> file, bool createIfNotExists)
            where T : class
        {
            var fileMediaKey = file.MediaKey ?? file.RootFile?.MediaKey;
            var containerName = fileMediaKey.Groups[0].EvaluateToString(entity);
            var id = fileMediaKey.Groups[1].EvaluateToString(entity);

            BlobClient cloudBlockBlob = null;
            if (!string.IsNullOrWhiteSpace(containerName))
            {
                cloudBlockBlob = await GetBlobAsync(containerName, id, createIfNotExists);
            }

            return cloudBlockBlob;
        }

        public static BlobSasBuilder AdminPolicy(TimeSpan? lifetime = null)
        {
            var builder = new BlobSasBuilder();
            builder.SetPermissions(
                BlobSasPermissions.Read |
                BlobSasPermissions.Create |
                BlobSasPermissions.Add |
                BlobSasPermissions.Write |
                BlobSasPermissions.Delete |
                BlobSasPermissions.List
            );
            return SetLifeTime(builder, lifetime);
        }

        public static BlobSasBuilder ReadOnlyPolicy(TimeSpan? lifetime = null)
        {
            var builder = new BlobSasBuilder();
            builder.SetPermissions(BlobSasPermissions.Read);
            return SetLifeTime(builder, lifetime);
        }

        protected static BlobSasBuilder SetLifeTime(BlobSasBuilder policy, TimeSpan? lifetime)
        {
            policy.StartsOn = DateTimeOffset.UtcNow;
            policy.ExpiresOn = lifetime.HasValue
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
                await fromBlob.StartCopyFromUriAsync(toBlob.Uri);
            }
        }

        public override async Task CloneUrlAsync(string fromUrl, string toUrl)
        {
            var fromBlob = new BlobClient(new Uri(new Uri(fromUrl).GetLeftPart(UriPartial.Path)));
            fromBlob = new BlobClient(
                ConnectionDetails.ConnectionString,
                fromBlob.BlobContainerName,
                fromBlob.Name);
            var toBlob = new BlobClient(new Uri(new Uri(toUrl).GetLeftPart(UriPartial.Path)));
            toBlob = new BlobClient(
                ConnectionDetails.ConnectionString,
                toBlob.BlobContainerName,
                toBlob.Name);
            if (await fromBlob.ExistsAsync())
            {
                await toBlob.GetParentBlobContainerClient().CreateIfNotExistsAsync();
                await toBlob.StartCopyFromUriAsync(fromBlob.Uri);
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