using Azure.Identity;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Services.StorageApi
{
    public class AzureBlob : IStorageCloud
    {
        private readonly IConfiguration _configuration;
        public AzureBlob(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateSASContainerUri()
        {
            //var accountName = _configuration["Azure:ACCOUNT_NAME"];
            //var accountKey = _configuration["Azure:ACCOUNT_KEY"];
            int time = int.Parse(_configuration["Azure:TIME_MINUTES"]);
            //StorageSharedKeyCredential sharedKey = new StorageSharedKeyCredential(accountName, accountKey);

            //var connect = "DefaultEndpointsProtocol=https;AccountName=garagefinder;AccountKey=7DdDTMbtkXtUJoOVIAlJYxsIaPBtcXG6hFo+qGaq16O8aHZxCx6deBjmdQM1PyU73qMrHgdNvcn9+AStriMDSg==;EndpointSuffix=core.windows.net";
            var connect = _configuration["Azure:CONNECTION"];
            BlobServiceClient blobServiceClient = new BlobServiceClient(connect);
            var container = _configuration["Azure:CONTAINER"];
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(container);
            // Create a SAS token that's valid for one day
            BlobSasBuilder sasBuilder = new BlobSasBuilder()
            {
                BlobContainerName = containerClient.Name,
                ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(time),
                Protocol = SasProtocol.HttpsAndHttp
            };

            sasBuilder.SetPermissions(BlobContainerSasPermissions.Read |
                BlobContainerSasPermissions.Write |
                BlobContainerSasPermissions.Add |
                BlobContainerSasPermissions.Create);

            // Use the key to get the SAS token
            //string sasToken = sasBuilder.ToSasQueryParameters(sharedKey).ToString();

            Uri sasURI = containerClient.GenerateSasUri(sasBuilder);
            return sasURI.ToString();
        }
    }
}
