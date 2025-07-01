<<<<<<< HEAD
using System.Text;
=======
﻿using CliWrap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

>>>>>>> b30244c (Corrected bug(s))

namespace AverageAudioBook.AudioConverter;

public static class AudioConverter
{
    // What we want to happen:
    // ffmpeg -activation_bytes <ACTIVATION_BYTES> -i <PATH_TO_FILE>/<AUDIBLE_FILE>.aax -c copy <PATH_TO_OUTPUT_FILE>/<NEW_AUDIO_FILE>.mp4
<<<<<<< HEAD
    public static async Task<string[]> ConvertAudible(string activation_bytes, string input_file, string ffmpeg_path = "ffmpeg", bool overwrite = true)
=======
    public async void ConvertAudible(string activation_bytes, string input_file, string ffmpeg_path = "ffmpeg")
>>>>>>> b30244c (Corrected bug(s))
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
<<<<<<< HEAD
                .Add($"{input_file}")
                .Add(overwriteFlag)
                .Add("-c")
                .Add("copy")
                .Add($"{output_dir}/{Utilities.ChangeSuffix(input_file)}");
=======
                .Add($"{input_dir}/{input_file}")
                .Add("-c copy")
                .Add($"{output_dir}/{ChangeSuffix(input_file)}");
>>>>>>> b30244c (Corrected bug(s))
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
<<<<<<< HEAD
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
=======
        string changed_Suffix = input_file.Split("/")[^1].Split(".")[0] + ".mp4";
        if (string.IsNullOrEmpty(changed_Suffix))
        {
            return "";
        }
        else
        {
            return changed_Suffix;
        }
>>>>>>> b30244c (Corrected bug(s))
    }

}
