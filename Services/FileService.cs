namespace UpByte.Console;

public class FileService
{
    public void InitializeRootFolder()
    {
        var rootFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        if (!Directory.Exists(rootFolder))
        {
            Directory.CreateDirectory(rootFolder);
        }
    }

    public void LoadConfigFile()
    {
        
    }
    
    public void SaveConfigFile()
    {
        
    }
}
