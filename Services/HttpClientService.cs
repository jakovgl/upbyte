namespace UpByte.Console;

public class HttpClientService
{
    public static (int, long, string) SendRequest(string url, int expectedResponseCode)
    {
        var (response, responseTime) = UpByteStopWatch.Execute(() => UpByteHttpClient.SendGetRequest(url));

        var statusCode = (int)response.StatusCode;
        var color = statusCode == expectedResponseCode ? "green" : "red";

        return (statusCode, responseTime, color);
    }
}