namespace UpByte.Console;

public static class UpByteHttpClient
{
    private static readonly HttpClient HttpClient = new();

    public static HttpResponseMessage SendGetRequest(string url)
    {
        return HttpClient.Send(new HttpRequestMessage(HttpMethod.Get, url));
    }
}