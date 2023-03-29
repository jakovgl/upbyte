using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using UpByte.Console;

internal abstract class Program
{
    private static IConfiguration _configuration;

    private static Task Main(string[] args)
    {
        ConfigureServices();

        var applications = StaticConfiguration.Applications;
        var table = new Table().Centered().Border(TableBorder.Ascii);
        var httpClient = SetupHttpClient();
        
        AnsiConsole.Live(table)
            .AutoClear(true)
            .Start(ctx =>
            {
                table.AddColumn("Name");
                table.AddColumn("Url");
                table.AddColumn("Code");
                table.AddColumn("Response Time (ms)");
                ctx.Refresh();

                while (true)
                {
                    table.Rows.Clear();
                    foreach (var application in applications)
                    {
                        string color;
                        int statusCode;
                        long responseTime;
                        try
                        {
                            (statusCode, responseTime, color) = SendRequest(httpClient, application.Url,
                                application.ExpectedResponseCode);
                        }
                        catch
                        {
                            statusCode = 500;
                            color = "red";
                            responseTime = 0;
                        }

                        table.AddRow(application.Name, application.Url, $"[{color}]{statusCode}[/]", responseTime.ToString());
                    }
                    
                    ctx.Refresh();
                    Thread.Sleep(1000);
                }
            });

        Console.ReadLine();
        return Task.CompletedTask;
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

    private static HttpClient SetupHttpClient()
    {
        HttpClient client = new();
        client.Timeout = TimeSpan.FromSeconds(60);
        return client;
    }

    private static (int, long, string) SendRequest (HttpClient httpClient, string url, int expectedResponseCode)
    {
        var stopWatch = new Stopwatch();
        stopWatch.Start();
        var response = httpClient.Send(new HttpRequestMessage(HttpMethod.Get, url));
        stopWatch.Stop();
        
        var statusCode = (int)response.StatusCode;
        var responseTime = stopWatch.ElapsedMilliseconds;
        var color = statusCode == expectedResponseCode ? "green" : "red";

        return (statusCode, responseTime, color);
    }
}