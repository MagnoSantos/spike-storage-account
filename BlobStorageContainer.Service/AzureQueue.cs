using BlobStorageContainer.Interface;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System.Linq;
using System.Threading.Tasks;

namespace BlobStorageContainer.Service
{
    public class AzureQueue : IAzureQueue
    {
        private readonly CloudQueueClient _queue;

        public AzureQueue(CloudStorageAccount storageAccount)
        {
            _queue = storageAccount.CreateCloudQueueClient();
        }

        public Task Criar(string nome)
        {
            return _queue
                .GetQueueReference(nome)
                .CreateIfNotExistsAsync();
        }

        public Task Deletar(string nome)
        {
            return _queue
                .GetQueueReference(nome)
                .DeleteIfExistsAsync();
        }

        public Task<bool> Existe(string nome)
        {
            return _queue
                .GetQueueReference(nome)
                .ExistsAsync();
        }

        public Task InserirMensagem(string nome, string mensagem)
        {
            return _queue
                .GetQueueReference(nome)
                .AddMessageAsync(
                message: new CloudQueueMessage(mensagem)
              );
        }

        public async Task<string> RemoverMensagem(string nome)
        {
            var mensagens = await _queue
                .GetQueueReference(nome)
                .GetMessagesAsync(1);

            return mensagens.FirstOrDefault()?.AsString;
        }
    }
}