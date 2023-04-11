namespace UpByte.Console.models;

public class Config
{
    public string Name { get; set; }
    public IEnumerable<Application> Applications { get; set; }
}