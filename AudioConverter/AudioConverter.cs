using System.Text;
using CliWrap;
using System;
using System.IO;

namespace AverageAudioBook.AudioConverter;

public class AudioConverter
{
    // What we want to happen:
    // ffmpeg -activation_bytes <ACTIVATION_BYTES> -i <PATH_TO_FILE>/<AUDIBLE_FILE>.aax -c copy <PATH_TO_OUTPUT_FILE>/<NEW_AUDIO_FILE>.mp4
    public async Task<string[]> ConvertAudible(string activation_bytes, string input_file, string ffmpeg_path = "ffmpeg", bool overwrite = true)
    {
        // Checks input file exists - otherwise fail
        if (!FileExists(path_to_file: input_file))
        {
            Console.WriteLine($"Input file does not exist.");
            Environment.Exit(-1);
        }

        var output_dir = Directory.GetCurrentDirectory() + "/Output";
        if (!VerifyDirectory(input_directory: output_dir))
        {
            CreateDirectory();
        }

        string overwriteFlag = "-";
        if (overwrite)
        {
            overwriteFlag += "y";
        }
        else
        {
            overwriteFlag += "n";
        }

        var stdOutBuffer = new StringBuilder();
        var stdErrBuffer = new StringBuilder();

        var result = await Cli.Wrap(ffmpeg_path)
            .WithArguments(args =>
            {
                args.Add("-activation_bytes")
                .Add(activation_bytes)
                .Add("-i")
                .Add($"{input_file}")
                .Add(overwriteFlag)
                .Add("-c")
                .Add("copy")
                .Add($"{output_dir}/{ChangeSuffix(input_file)}");
            }).WithWorkingDirectory(".")
            .WithStandardOutputPipe(PipeTarget.ToStringBuilder(stdOutBuffer))
            .WithStandardErrorPipe(PipeTarget.ToStringBuilder(stdErrBuffer))
            .ExecuteAsync();

        var stdOut = stdOutBuffer.ToString();
        var stdErr = stdErrBuffer.ToString();
        string[] results = { stdOut, stdErr };
        return results;
    }

    // Do non-audible conversions
    public static async Task<string[]> ConvertAudio(string input_file, string ffmpeg_path = "ffmpeg", bool overwrite = true)
    {
        // Checks input file exists - otherwise fail
        if (!FileExists(path_to_file: input_file))
        {
            Console.WriteLine($"Input file does not exist.");
            Environment.Exit(-1);
        }

        var output_dir = Directory.GetCurrentDirectory() + "/Output";
        if (!VerifyDirectory(input_directory: output_dir))
        {
            CreateDirectory();
        }

        string overwriteFlag = "-";
        if (overwrite)
        {
            overwriteFlag += "y";
        }
        else
        {
            overwriteFlag += "n";
        }

        var stdOutBuffer = new StringBuilder();
        var stdErrBuffer = new StringBuilder();

        var result = await Cli.Wrap(ffmpeg_path)
            .WithArguments(args =>
            {
                args.Add("-i")
                .Add($"{input_file}")
                .Add(overwriteFlag)
                .Add("-c")
                .Add("copy")
                .Add($"{output_dir}/{ChangeSuffix(input_file)}");
            }).WithWorkingDirectory(".")
            .WithStandardOutputPipe(PipeTarget.ToStringBuilder(stdOutBuffer))
            .WithStandardErrorPipe(PipeTarget.ToStringBuilder(stdErrBuffer))
            .ExecuteAsync();

        var stdOut = stdOutBuffer.ToString();
        var stdErr = stdErrBuffer.ToString();
        string[] results = [stdOut, stdErr];
        return results;
    }

    private static string ChangeSuffix(string input_file)
    {
        string fileName = Path.GetFileName(input_file);
        switch (Path.GetExtension(fileName))
        {
            case ".aax":
                return Path.ChangeExtension(fileName, ".m4b");
            case ".mp4":
                return Path.ChangeExtension(fileName, ".mp3");
            case ".mp3":
                return Path.ChangeExtension(fileName, ".mp4");
            default:
                return "";
        }
    }

    private static bool VerifyDirectory(string input_directory)
    {
        if (Directory.Exists(input_directory)) { return true; }
        return false;
    }

    private static bool FileExists(string path_to_file)
    {
        if (File.Exists(path_to_file))
        {
            return true;
        }
        return false;
    }

    private static void CreateDirectory()
    {
        string newDirectory = Directory.GetCurrentDirectory() + "/Output";
        Directory.CreateDirectory(newDirectory);
        Console.WriteLine($"Created new directory at '{newDirectory}'");
    }

}
