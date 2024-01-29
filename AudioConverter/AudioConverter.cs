using CliWrap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;


namespace AverageAudioBook.AudioConverter;

public class AudioConverter
{
    // What we want to happen:
    // ffmpeg -activation_bytes <ACTIVATION_BYTES> -i <PATH_TO_FILE>/<AUDIBLE_FILE>.aax -c copy <PATH_TO_OUTPUT_FILE>/<NEW_AUDIO_FILE>.mp4
    public async void ConvertAudible(string activation_bytes, string input_file, string ffmpeg_path = "ffmpeg")
    {
        var input_dir = "./Input";
        var output_dir = "./Output";

        var result = await Cli.Wrap(ffmpeg_path)
            .WithArguments(args =>
            {
                args.Add("-activation_bytes")
                .Add(activation_bytes)
                .Add("-i")
                .Add($"{input_dir}/{input_file}")
                .Add("-c copy")
                .Add($"{output_dir}/{ChangeSuffix(input_file)}");
            }).WithWorkingDirectory(".")
            .ExecuteAsync();
    }

    private string ChangeSuffix(string input_file)
    {
        string changed_Suffix = input_file.Split("/")[^1].Split(".")[0] + ".mp4";
        if (string.IsNullOrEmpty(changed_Suffix))
        {
            return "";
        }
        else
        {
            return changed_Suffix;
        }
    }

}
