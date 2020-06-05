using System.Threading.Tasks;

namespace BlobStorageContainer.Interface
{
    public interface IAzureQueue
    {
        Task Criar(string nome);

        Task Deletar(string nome);

        Task<bool> Existe(string nome);

        Task InserirMensagem(string nome, string mensagem);

        Task<string> RemoverMensagem(string nome);
    }
}