using import.Commands;
using import.Infrastructure;
using import.Interfaces;
using import.Loaders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.CommandLine;

class Program
{
    static async Task Main(string[] args)
    {

        // Setup Dependency Injection         
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var rootCommand = serviceProvider.GetRequiredService<RootCommand>();
        rootCommand.Description = "SaaS Products Import Tool";

        rootCommand.AddCommand(serviceProvider.GetRequiredService<CapterraCommand>());

        await rootCommand.InvokeAsync(args);
    }


    private static void ConfigureServices(ServiceCollection serviceCollection)
    {
        serviceCollection.AddLogging();
        serviceCollection.AddSingleton<RootCommand>();
        serviceCollection.AddSingleton<CapterraCommand>();
        serviceCollection.AddSingleton<ICapterraLoader, CapterraLoader>();
        serviceCollection.AddSingleton<IRepository, InventoryRepository>();

    }
}