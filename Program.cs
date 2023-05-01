using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UpByte.Console;

internal abstract class Program
{
    private static IConfiguration _configuration;

    private static Task Main(string[] args)
    {
        var serviceProvider = ConfigureServices();
        
        var displayService = serviceProvider.GetRequiredService<DisplayService>();
        var configService = serviceProvider.GetRequiredService<ConfigService>();
        
        Console.Clear();

        var config = configService.LoadConfig();

        if (config == null)
        {
            config = displayService.DisplayConfigCreateScreen();
            configService.SaveConfig(config);
            Console.Clear();
        }

        do
        {
            displayService.Display(config);
            config = displayService.DisplayConfigCreateScreen();
            configService.SaveConfig(config);
            Console.Clear();
        } while (true);
    }

    private static ServiceProvider ConfigureServices()
    {
        _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .Build();

        var serviceProvider = new ServiceCollection()
            .AddSingleton(_configuration)
            .AddScoped<HttpClientService>()
            .AddScoped<DisplayService>()
            .AddScoped<ConfigService>()
            .AddScoped<FileService>()
            .BuildServiceProvider();

        return serviceProvider;
    }
}