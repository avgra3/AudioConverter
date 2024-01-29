using System.Text;

namespace AverageAudioBook.AudioConverter;

public static class AudioConverter
{
    // What we want to happen:
    // ffmpeg -activation_bytes <ACTIVATION_BYTES> -i <PATH_TO_FILE>/<AUDIBLE_FILE>.aax -c copy <PATH_TO_OUTPUT_FILE>/<NEW_AUDIO_FILE>.mp4
    public static async Task<string[]> ConvertAudible(string activation_bytes, string input_file, string ffmpeg_path = "ffmpeg", bool overwrite = true)
    {
        // Checks input file exists - otherwise fail
        if (!Utilities.FileExists(pathToFile: input_file))
        {
            Console.WriteLine($"Input file does not exist.");
            Environment.Exit(-1);
        }

        var output_dir = Directory.GetCurrentDirectory() + "/Output";
        if (!Utilities.VerifyDirectory(inputDirectory: output_dir))
        {
            Utilities.CreateDirectory();
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
                .Add($"{output_dir}/{Utilities.ChangeSuffix(input_file)}");
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
        if (!Utilities.FileExists(pathToFile: input_file))
        {
            Console.WriteLine($"Input file does not exist.");
            Environment.Exit(-1);
        }

        var output_dir = Directory.GetCurrentDirectory() + "/Output";
        if (!Utilities.VerifyDirectory(inputDirectory: output_dir))
        {
            Utilities.CreateDirectory();
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
                .Add($"{output_dir}/{Utilities.ChangeSuffix(input_file)}");
            }).WithWorkingDirectory(".")
            .WithStandardOutputPipe(PipeTarget.ToStringBuilder(stdOutBuffer))
            .WithStandardErrorPipe(PipeTarget.ToStringBuilder(stdErrBuffer))
            .ExecuteAsync();

        var stdOut = stdOutBuffer.ToString();
        var stdErr = stdErrBuffer.ToString();
        string[] results = [stdOut, stdErr];
        return results;
    }

}
