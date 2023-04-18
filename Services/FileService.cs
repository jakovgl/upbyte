namespace UpByte.Console;

public class FileService
{
    public static void InitializeRootFolder()
    {
        var rootFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        if (!Directory.Exists(rootFolder))
        {
            Directory.CreateDirectory(rootFolder);
        }
    }

    public static void LoadConfigFile()
    {
        
    }
    
    public static void SaveConfigFile()
    {
        
    }
}
