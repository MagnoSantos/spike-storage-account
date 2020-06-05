using BlobStorageContainer.Interface;
using BlobStorageContainer.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BlobStorageContainer
{
    public static class ServiceColletionExtensions
    {
        private static readonly ServiceCollection _serviceCollection;
        private static readonly IConfiguration _configuration;

        static ServiceColletionExtensions()
        {
            _serviceCollection = new ServiceCollection();
            _configuration = ConfigurarVariaveisAmbiente();
        }

        private static IConfiguration ConfigurarVariaveisAmbiente()
        {
            var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

            return builder.Build();
        }

        public static ServiceCollection ConfigurarDependencias()
        {
            _serviceCollection.AddTransient<IAzureQueue, AzureQueue>();
            _serviceCollection.AddTransient<IBlobStorage, BlobStorage>();
            _serviceCollection.AddSingleton(_serviceCollection =>
            {
                return CloudStorageAccount.Parse(_configuration.GetConnectionString("AzureStorage"));
            });

            return _serviceCollection;
        }

        public static ServiceCollection ConfigurarAplicacao()
        {
            _serviceCollection
                .Configure<ConfiguracoesAplicacao>(options => 
                {
                    options.StorageAccountConnectionString = _configuration.GetConnectionString("AzureStorage");
                });

            return _serviceCollection;
        }

        public static ServiceProvider GetContainerDependencias()
        {
            return _serviceCollection.BuildServiceProvider();
        }
    }
}