using Spectre.Console;
using UpByte.Console.models;

namespace UpByte.Console;

public class DisplayService
{
    private readonly HttpClientService _httpClientService;

    public DisplayService(HttpClientService httpClientService)
    {
        _httpClientService = httpClientService;
    }

    public void Display(Config config)
    {
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
                    foreach (var application in config.Applications)
                    {
                        string color;
                        int statusCode;
                        long responseTime;
                        try
                        {
                            (statusCode, responseTime, color) =
                                _httpClientService.SendRequest(application.Url, application.ExpectedResponseCode);
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

        System.Console.ReadLine();
    }
}