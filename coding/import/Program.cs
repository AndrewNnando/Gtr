using System.CommandLine;

class Program
{
    static async Task Main(string[] args)
    {
        var rootCommand = new RootCommand();
        rootCommand.Description = "SaaS Products Import Tool";
        rootCommand.SetHandler(() => Console.WriteLine("Hello World!"));
        await rootCommand.InvokeAsync(args);
    }
}