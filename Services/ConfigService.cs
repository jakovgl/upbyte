using UpByte.Console.models;

namespace UpByte.Console;

public class ConfigService
{
    private readonly FileService _fileService;

    public ConfigService(FileService fileService)
    {
        _fileService = fileService;
    }

    public Config LoadDefaultConfig()
    {
        return new Config
        {
            Name = "UpByte",
            Applications = StaticConfiguration.Applications
        };
    }
}