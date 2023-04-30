using Newtonsoft.Json;
using UpByte.Console.models;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace UpByte.Console;

public class ConfigService
{
    private readonly FileService _fileService;

    public ConfigService(FileService fileService)
    {
        _fileService = fileService;
    }

    public Config LoadConfig()
    {
        var json = _fileService.LoadConfigFile();

        if (json == null)
            return null;
        
        return JsonConvert.DeserializeObject<Config>(json);
    }

    public void SaveConfig(Config config)
    {
        var jsonConfig = JsonSerializer.Serialize(config);
        
        _fileService.SaveConfigFile(jsonConfig);
    }
}