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
                            (statusCode, responseTime, color) =
                                SendRequest(application.Url, application.ExpectedResponseCode);
                        }
                        catch
                        {
                            statusCode = 500;
                            color = "red";
                            responseTime = 0;
                        }

                        table.AddRow(application.Name, application.Url, $"[{color}]{statusCode}[/]",
                            responseTime.ToString());
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

    private static (int, long, string) SendRequest(string url, int expectedResponseCode)
    {
        var (response, responseTime) = UpByteStopWatch.Execute(() => UpByteHttpClient.SendGetRequest(url));

        var statusCode = (int)response.StatusCode;
        var color = statusCode == expectedResponseCode ? "green" : "red";

        return (statusCode, responseTime, color);
    }
}