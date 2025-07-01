using Spectre.Console;

namespace AudioConverter.Cli;

public class Cli
{
	public static async Task Main()
	{
		AnsiConsole.Markup("[underline red]WELCOME TO AUDIO CONVERTER[/]\n");
		// var audio = new AverageAudioBook.AudioConverter.AudioConverter();
		string[] inputs = Inputs();
		bool overwrite = false;
		if (inputs[3] == "y")
		{
			overwrite = true;
		}
		var results = await AverageAudioBook.AudioConverter.AudioConverter.ConvertAudible(activation_bytes: inputs[0], input_file: inputs[1], ffmpeg_path: inputs[2], overwrite: overwrite);
		// var results = await AudioConverter.ConvertAudible(activation_bytes: inputs[0], input_file: inputs[1], ffmpeg_path: inputs[2], overwrite: overwrite);
		AnsiConsole.Markup("\n[green]DONE![/]\n");
	}

	private static string[] Inputs()
	{
		string orange = "#ff4f33";

		var audioFile = AnsiConsole.Prompt(
		    new TextPrompt<string>("Full path to audio file:")
		    );
		bool overwriteBool = false;
		if (FileExists(audioFile))
		{
			overwriteBool = AnsiConsole.Prompt(
			    new SelectionPrompt<bool>()
			    .Title($"[{orange}]The expected output file exists, do you want to overwrite?[/]")
			    .PageSize(10)
			    .MoreChoicesText("[grey]Select True or False to overwrite output file if exists.[/]")
			    .AddChoices([true, false]));
		}
		string overwriteFlag = overwriteBool ? "y" : "n";
		AnsiConsole.Markup($"Overwrite file: {overwriteBool}\n");
		if (audioFile.Trim() == string.Empty)
		{
			AnsiConsole.Markup("[red]A full path to the audio file is necessary in order for the app to work. Please try again![/]");
			Environment.Exit(-1);
		}
		var activationBytes = AnsiConsole.Prompt(
		    new TextPrompt<string>("[[Optional]] Activation Bytes (leave blank if environment variable):")
		    .AllowEmpty());
		if (activationBytes.Trim() == string.Empty)
		{
			if (Environment.GetEnvironmentVariable("ACTIVATION_BYTES") != null)
			{
				activationBytes = Environment.GetEnvironmentVariable("ACTIVATION_BYTES");
			}
			else
			{
				AnsiConsole.Markup("[red]Activation bytes not provided and the system environment variable ACTIVATION_BYTES was not set. Please try again![/]");
				Environment.Exit(-1);
			}
		}
		var ffmpegLocation = AnsiConsole.Prompt(
		    new TextPrompt<string>("[[Optional]] FFMPEG path (leave blank if on system path):")
		    .AllowEmpty());
		if (ffmpegLocation.Trim() == string.Empty)
		{
			ffmpegLocation = "ffmpeg";
		}

		if (string.IsNullOrEmpty(activationBytes))
		{
			AnsiConsole.Markup("[red]Activation bytes cannot be a null value.[/]\n");
			Environment.Exit(-1);
		}

		string[] inputs = [activationBytes, audioFile, ffmpegLocation, overwriteFlag];
		return inputs;
	}

	private static bool FileExists(string file)
	{
		string fileOutput = Path.ChangeExtension(file, ".m4b");
		fileOutput = Path.GetFileName(fileOutput);
		return File.Exists(Directory.GetCurrentDirectory() + "/Output/" + fileOutput);
	}
}
