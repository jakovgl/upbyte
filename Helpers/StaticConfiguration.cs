using Microsoft.Extensions.Configuration;
using UpByte.Console.models;

namespace UpByte.Console;

public class StaticConfiguration
{
    private static IConfiguration _configuration;

    public static List<Application> Applications => _configuration.GetSection("applications").Get<List<Application>>();

    public static void Initialize(IConfiguration configuration)
    {
        _configuration = configuration;
    }
}