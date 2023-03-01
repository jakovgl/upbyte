using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using UpByte.Console;

internal abstract class Program
{
    private static IConfiguration _configuration;

    private static void Main(string[] args)
    {
        ConfigureServices();

        var applications = StaticConfiguration.Applications;
        
        Console.WriteLine(applications.Count);

        var table = new Table().Centered();

        AnsiConsole.Live(table)
            .AutoClear(false)
            .Start(ctx =>
            {
                table.AddColumn("Name");
                table.AddColumn("Url");
                ctx.Refresh();
                Thread.Sleep(1000);

                while (true)
                {
                    table.Rows.Clear();
                    table.AddRow(GetRandomNumber().ToString(), GetRandomNumber().ToString());
                    ctx.Refresh();
                    Thread.Sleep(1000);
                }
            });

        Console.ReadLine();
    }
    
    private static void ConfigureServices()
    {
        _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var serviceProvider = new ServiceCollection()
            .AddSingleton(_configuration)
            .BuildServiceProvider();

        var configuration = serviceProvider.GetRequiredService<IConfiguration>();

        StaticConfiguration.Initialize(configuration);

    }

    private static int GetRandomNumber()
    {
        var random = new Random();
        return random.Next();
    }
}