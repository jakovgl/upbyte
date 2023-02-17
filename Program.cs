using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UpByte.Console;

internal abstract class Program
{
    private static IConfiguration _configuration;

    private static void Main(string[] args)
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

        Console.WriteLine(StaticConfiguration.Text);
    }
}