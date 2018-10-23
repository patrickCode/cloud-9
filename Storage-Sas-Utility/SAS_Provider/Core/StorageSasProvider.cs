using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace SAS.Provider.Core
{
    public class StorageSasProvider
    {
        private CloudStorageAccount _storageAccount;

        public StorageSasProvider(string storageAcccountName, string storageAccountKey)
        {
            var storageCredentials = new StorageCredentials(storageAcccountName, storageAccountKey);
            _storageAccount = new CloudStorageAccount(storageCredentials, useHttps: true);
        }

        public async Task<string> CreateSasForContainer(string containerName, double expireIn)
        {
            var container = await CreateBlobContainer(containerName);
            var sharedPolicy = new SharedAccessBlobPolicy()
            {
                Permissions = SharedAccessBlobPermissions.Write | SharedAccessBlobPermissions.List,
                SharedAccessExpiryTime = DateTime.Now.AddHours(expireIn)
            };

            var token = container.GetSharedAccessSignature(sharedPolicy, null);
            return container.Uri + token;
        }

        private async Task<CloudBlobContainer> CreateBlobContainer(string containerName)
        {
            var cloudBlobClient = _storageAccount.CreateCloudBlobClient();
            var container = cloudBlobClient.GetContainerReference(containerName);
            var isContainerCreated = await container.ExistsAsync();
            if (isContainerCreated)
                return container;
            throw new Exception($"No container with name {containerName} found in the application");
        }
    }
}
