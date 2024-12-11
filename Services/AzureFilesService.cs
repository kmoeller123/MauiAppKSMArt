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
            await blobClient.UploadAsync(uploadFileStream, true);
            uploadFileStream.Close();
        }

     }    
}
