using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using System.Drawing;

namespace MauiAppKSMArt
{    
    public class AzureFilesService
    {
        BlobServiceClient _blobServiceClient;
        BlobContainerClient _blobContainerClient;

        string _AzureConnectionString = KSMAppSetting.AzureConnectionString;
        string ContainerName = KSMAppSetting.AzureContainer;

        public AzureFilesService()
        {
            _blobServiceClient = new BlobServiceClient(_AzureConnectionString);
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);
        }

        public async Task UploadFileAsync(string filePath)
        {
            BlobClient blobClient = _blobContainerClient.GetBlobClient(Path.GetFileName(filePath));

            using FileStream uploadFileStream = File.OpenRead(filePath);
            var me = await blobClient.UploadAsync(uploadFileStream, true);
            
            uploadFileStream.Close();            
        }

        internal void DeleteBlobsFromContainer(Uri blobUri, List<string> fileNames)
        {
            foreach (var fileName in fileNames)
            {
                var uri = new Uri(blobUri, fileName);
                BlobClient blobClient = new BlobClient(uri);
                //blobClient.Delete(DeleteSnapshotsOption.IncludeSnapshots);
                blobClient.DeleteIfExists(DeleteSnapshotsOption.IncludeSnapshots);
            }
        }

    }    
}
