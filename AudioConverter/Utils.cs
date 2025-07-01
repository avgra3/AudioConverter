namespace AverageAudioBook.AudioConverter;

public static class Utilities
{
    public static string ChangeSuffix(string inputFile)
    {
        string fileName = Path.GetFileName(inputFile);
        return Path.GetExtension(fileName) switch
        {
            ".aax" => CombineDirAndFile(directory: Path.GetDirectoryName(inputFile),
                filename: Path.ChangeExtension(fileName, ".m4b")),
            ".mp4" => CombineDirAndFile(directory: Path.GetDirectoryName(inputFile),
                filename: Path.ChangeExtension(fileName, ".mp3")),
            ".mp3" => CombineDirAndFile(directory: Path.GetDirectoryName(inputFile),
                filename: Path.ChangeExtension(fileName, ".mp4")),
            _ => ""
        };
    }

    private static string CombineDirAndFile(string filename, string? directory)
    {
        if (directory != null) return Path.Combine(directory, filename);
        return "";
    }

    public static bool VerifyDirectory(string inputDirectory)
    {
        if (Directory.Exists(inputDirectory)) { return true; }
        return false;
    }

    public static bool FileExists(string pathToFile)
    {
        if (File.Exists(pathToFile))
        {
            return true;
        }
        return false;
    }

    public static void CreateDirectory()
    {
        string newDirectory = Directory.GetCurrentDirectory() + "/Output";
        Directory.CreateDirectory(newDirectory);
        Console.WriteLine($"Created new directory at '{newDirectory}'");
    }
}

