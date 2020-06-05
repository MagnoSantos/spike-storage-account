using BlobStorageContainer.Interface;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Mime;

namespace BlobStorageContainer
{
    public class Program
    {
        private static void Main(string[] args)
        {
            ServiceColletionExtensions.ConfigurarDependencias();
            ServiceColletionExtensions.ConfigurarAplicacao();
            var container = ServiceColletionExtensions.GetContainerDependencias();

            var blob = container.GetRequiredService<IBlobStorage>();

            //Se existir blob container
            //Inserir arquivo no blob
        }
    }
}