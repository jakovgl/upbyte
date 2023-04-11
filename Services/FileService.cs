namespace UpByte.Console;

public class FileHandler
{
    public void InitializeRootFolder()
    {
        var rootFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        if (!Directory.Exists(rootFolder))
        {
            Directory.CreateDirectory(rootFolder);
        }
    }

    public void LoadConfigFiles()
    {
        
    }
    
    public void CreateConfigFile()
    {
        
    }
}