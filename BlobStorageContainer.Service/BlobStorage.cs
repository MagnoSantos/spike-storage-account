using BlobStorageContainer.Interface;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Threading.Tasks;

namespace BlobStorageContainer.Service
{
    public class BlobStorage : IBlobStorage
    {
        private readonly CloudBlobClient _blobClient;

        public BlobStorage(CloudStorageAccount storageAccount)
        {
            _blobClient = storageAccount.CreateCloudBlobClient();
        }

        public Task Criar(string nomeContainer)
        {
            return _blobClient
                .GetContainerReference(nomeContainer)
                .CreateIfNotExistsAsync();
        }

        public Task Deletar(string nomeContainer)
        {
            return _blobClient
                .GetContainerReference(nomeContainer)
                .DeleteIfExistsAsync();
        }

        public Task<bool> ExisteContainer(string nomeContainer)
        {
            return _blobClient
                .GetContainerReference(nomeContainer)
                .ExistsAsync();
        }

        public async Task<string> Inserir(
            string nomeContainer,
            string nomeArquivo,
            byte[] rawbytes, 
            string mimetype
        )
        {
            var cloudBlockBlob = _blobClient
                .GetContainerReference(nomeContainer)
                .GetBlockBlobReference(nomeArquivo);

            cloudBlockBlob.Properties.ContentType = mimetype;

            using var stream = new MemoryStream(rawbytes);
            await cloudBlockBlob.UploadFromStreamAsync(stream);

            return cloudBlockBlob.Uri.ToString();
        }
    }
}