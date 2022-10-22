using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using BlazorApp.Shared;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;

namespace ApiIsolated
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = new HostBuilder()
                            .ConfigureFunctionsWorkerDefaults()
                            .ConfigureServices(services =>
                            {
                                services.AddSingleton(sp =>
                                {
                                    return new MongoClient(Environment.GetEnvironmentVariable("AZURE_COSMOS_ENDPOINT"));
                                });
                                services.AddSingleton<DefinitionsRepository>();

                            })
                            .Build();
            await host.RunAsync();
        }
    }
}