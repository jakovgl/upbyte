using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UpByte.Console;
using UpByte.Console.models;

internal abstract class Program
{
    private static IConfiguration _configuration;

    private static Task Main(string[] args)
    {
        var serviceProvider = ConfigureServices();
        var displayService = serviceProvider.GetRequiredService<DisplayService>();

        var applications = StaticConfiguration.Applications;
        var config = new Config
        {
            Name = "UpByte",
            Applications = applications
        };
        
        displayService.Display(config);
        
        return Task.CompletedTask;
    }

    private static ServiceProvider ConfigureServices()
    {
        _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var serviceProvider = new ServiceCollection()
            .AddSingleton(_configuration)
            .AddScoped<HttpClientService>()
            .AddScoped<DisplayService>()
            .AddScoped<ConfigService>()
            .AddScoped<FileService>()
            .BuildServiceProvider();

        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        StaticConfiguration.Initialize(configuration);

        return serviceProvider;
    }
}