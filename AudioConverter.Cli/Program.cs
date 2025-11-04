using Spectre.Console;

namespace AudioConverter.Cli;

public class Cli
{
	public static async Task Main()
	{
		AnsiConsole.Markup("[underline green]WELCOME TO AUDIO CONVERTER[/]\n");
		string[] inputs = Inputs();
		bool overwrite = false;
		if (inputs[3] == "y")
		{
			overwrite = true;
		}
		AverageAudioBook.AudioConverter.AudioConverter converter = new();
		var results = await converter.ConvertAudible(activation_bytes: inputs[0], input_file: inputs[1], ffmpeg_path: inputs[2], overwrite: overwrite, output_suffix: inputs[4]);
		if (results[0] != "")
		{
			AnsiConsole.MarkupInterpolated($"[red]ERROR: [/]{results[0]}\n");

		}
		AnsiConsole.Markup("\n[green]DONE![/]\n");
		AnsiConsole.MarkupInterpolated($"[yellow]Output: [/]{results[1]}\n");
	}

	private static string[] Inputs()
	{
		string orange = "#ff4f33";

		var audioFile = AnsiConsole.Prompt(
		    new TextPrompt<string>("Full path to audio file [teal]change the selected if you want to convert a different file[/]:")
		    .DefaultValue(GetFilePath.OpenFileExplorerAndGetPath())
		    );
		var wantedOutput = AnsiConsole.Prompt(
				new SelectionPrompt<string>()
				.Title("What output file extension do you expect?")
				.PageSize(10)
				.AddChoices(["m4b", "mp4"]));
		bool overwriteBool = false;
		if (FileExists(file: audioFile, expected_extension: "." + wantedOutput))
		{
			overwriteBool = AnsiConsole.Prompt(
			    new SelectionPrompt<bool>()
			    .Title($"[{orange}]The expected output file exists, do you want to overwrite?[/]")
			    .PageSize(10)
			    .MoreChoicesText("[grey]Select True or False to overwrite output file if exists.[/]")
			    .AddChoices([true, false]));
			// Just exit the program if the user selects false.
			if (!overwriteBool)
			{
				AnsiConsole.Markup("[teal]You elected to not overwrite the current file of the same name. Now exiting the program. Goodbye![/]\n");
				Environment.Exit(-1);

			}
		}
		string overwriteFlag = overwriteBool ? "y" : "n";
		if (audioFile.Trim() == string.Empty)
		{
			AnsiConsole.Markup("[red]A full path to the audio file is necessary in order for the app to work. Please try again![/]");
			Environment.Exit(-1);
		}
		var activationBytes = AnsiConsole.Prompt(
		    new TextPrompt<string>("[gray][[Optional]][/] Activation Bytes (leave blank if environment variable):")
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
		    new TextPrompt<string>("[grey][[Optional]][/] FFMPEG path (leave blank if on system path):")
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

		string[] inputs = [activationBytes, audioFile, ffmpegLocation, overwriteFlag, "." + wantedOutput];
		return inputs;
	}

	private static bool FileExists(string file, string expected_extension = ".m4b")
	{
		string fileOutput = Path.ChangeExtension(file, expected_extension);
		fileOutput = Path.GetFileName(fileOutput);
		return File.Exists(Directory.GetCurrentDirectory() + "/Output/" + fileOutput);
	}
}
