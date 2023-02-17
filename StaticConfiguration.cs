using Microsoft.Extensions.Configuration;

namespace UpByte.Console;

public class StaticConfiguration
{
    private static IConfiguration _configuration;
    
    public static string Text => _configuration.GetSection("Text").Value;
    
    public static void Initialize(IConfiguration configuration)
    {
        _configuration = configuration;
    }
}
