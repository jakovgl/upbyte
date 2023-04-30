namespace UpByte.Console;

public class FileService
{
    private readonly string _configFilePath =
        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/upbyte.json"; 
    
    public string LoadConfigFile()
    {
        if (!File.Exists(_configFilePath))
            return null;

        using var reader = new StreamReader(_configFilePath);
        var json = reader.ReadToEnd();

        return json;
    }
    
    public void SaveConfigFile(string config)
    {
        File.WriteAllText(_configFilePath, config);
    }
}
