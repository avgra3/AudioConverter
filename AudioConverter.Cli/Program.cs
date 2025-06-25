using System.Threading.Tasks;
using AverageAudioBook.AudioConverter;

namespace AudioConverter.Cli;

public class Cli
{

	public static async Task Main()
	{
		var audio = new AverageAudioBook.AudioConverter.AudioConverter();
		string[] inputs = Inputs();
		var results = await audio.ConvertAudible(activation_bytes: inputs[0], input_file: inputs[1], ffmpeg_path: inputs[2]);
		Console.WriteLine("DONE!");
		Console.ReadKey();
	}


	private static string[] Inputs()
	{
		Console.Write("Activation bytes: ");
		string activationBytes = Console.ReadLine();
		Console.Write("Input audio file path: ");
		string audioFile = Console.ReadLine();
		Console.Write("FFMPEG path (leave blank if on system path): ");
		string ffmpegLocation = Console.ReadLine();

		if (ffmpegLocation.Trim() == string.Empty)
		{
			ffmpegLocation = "ffmpeg";
		}

		string[] inputs = { activationBytes, audioFile, ffmpegLocation };
		return inputs;

	}
}
