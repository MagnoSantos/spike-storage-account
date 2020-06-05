using System.Threading.Tasks;

namespace BlobStorageContainer.Interface
{
    public interface IBlobStorage
    {
        Task Criar(string nomeContainer);

        Task Deletar(string nomeContainer);

        Task<bool> ExisteContainer(string nomeContainer);

        Task<string> Inserir(string nomeContainer, string nomeArquivo, byte[] rawbytes, string mimetype);
    }
}