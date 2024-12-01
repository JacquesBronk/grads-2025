namespace Retro.Seeder;

public class FileCache
{
    private string[]? _files;
    
    public string[]? GetFiles(string path)
    {
        _files = Directory.GetFiles(path, "*.json");
        return _files;
    }

    public string ReadFileByName(string name)
    {
        if (_files == null)
        {
            throw new DirectoryNotFoundException("No files found");
        }
        
        if (_files?.Length == 0)
        {
            throw new FileNotFoundException("No files found for {Name}", name);
        }
        
        var file = _files?.FirstOrDefault(f => Path.GetFileNameWithoutExtension(f) == name);
        
        if (file == null)
        {
            throw new FileNotFoundException("File not found", name);
        }
        
        return File.ReadAllText(file);
    }
}