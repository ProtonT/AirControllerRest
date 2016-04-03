using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace AirControllerWebService.Controllers.REST
{
    public class ImageController : ApiController
    {
        public Task<HttpResponseMessage> Post()
        {
            HttpRequestMessage request = this.Request;
            if (!request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var streamProvider = new MultipartMemoryStreamProvider();
            var task = request.Content.ReadAsMultipartAsync(streamProvider).ContinueWith<HttpResponseMessage>(o =>
            {
                foreach (var item in streamProvider.Contents)
                {
                    Stream stream = item.ReadAsStreamAsync().Result;

                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                    CloudBlobContainer container = blobClient.GetContainerReference("images");

                    container.CreateIfNotExists();
                    container.SetPermissions(new BlobContainerPermissions
                    {
                        PublicAccess = BlobContainerPublicAccessType.Blob
                    });

                    CloudBlockBlob blob = container.GetBlockBlobReference("image.png");

                    blob.UploadFromStream(stream);

                    string couponThumbnailUrl = blob.Uri.AbsoluteUri;
                }

                return new HttpResponseMessage()
                {
                    Content = new StringContent("File uploaded.")
                };
            });

            return task;
        }

        public HttpResponseMessage Get()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference("images");

            CloudBlockBlob blockBlob = container.GetBlockBlobReference("image.png");

            var imgUri = blockBlob.Uri;

            return Request.CreateResponse(HttpStatusCode.OK, imgUri);
        }
    }
}
