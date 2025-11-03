using System.Diagnostics;

namespace AudioConverter.Cli;

public class GetFilePath
{
	public static string OpenFileExplorerAndGetPath()
	{
		if (OperatingSystem.IsLinux())
		{
			return LinuxOpenFileExplorerAndGetPath();
		}
		if (OperatingSystem.IsWindows())
		{
			return WindowsOpenFileExplorerAndGetPath();
		}
		if (OperatingSystem.IsMacOS())
		{
			return MacOpenFileExplorerAndGetPath();
		}
		throw new PlatformNotSupportedException();

	}

	private static string WindowsOpenFileExplorerAndGetPath()
	{
		// Currently unable to do this
		return "";
	}

	private static string LinuxOpenFileExplorerAndGetPath()
	{
		try
		{
			var process = new Process
			{
				StartInfo = new ProcessStartInfo
				{
					FileName = "zenity",
					Arguments = "--file-selection --title=\"Select a File\"",
					RedirectStandardOutput = true,
					RedirectStandardError = true,
					UseShellExecute = false,
					CreateNoWindow = true,
				}
			};

			process.Start();
			string output = process.StandardOutput.ReadToEnd().Trim();
			string error = process.StandardError.ReadToEnd().Trim();
			process.WaitForExit();

			if (process.ExitCode == 0 && !string.IsNullOrEmpty(output))
			{
				return output;
			}
			else
			{
				Console.WriteLine("File selection cancelled or error: " + error);
				return "";
			}
		}
		catch (Exception e)
		{
			Console.WriteLine("Error launching file dialog: " + e.Message);
			return "";
		}
	}

	private static string MacOpenFileExplorerAndGetPath()
	{
		string title = "Select a file";
		string defaultDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
		try
		{
			var args = new System.Text.StringBuilder();
			args.Append($"choose a file with prompt \"{Escape(title)}\"");
			args.Append($" default location \"{Escape(defaultDirectory)}\"");
			args.Append(" as text");

			var process = new Process
			{
				StartInfo = new ProcessStartInfo
				{
					FileName = "osascript",
					Arguments = $"-e 'tell application \"System Events\" to return POSIX path of ({args})'",
					UseShellExecute = false,
					RedirectStandardOutput = true,
					RedirectStandardError = true,
					CreateNoWindow = true
				}
			};

			process.Start();
			string output = process.StandardOutput.ReadToEnd().Trim();
			string error = process.StandardError.ReadToEnd().Trim();
			process.WaitForExit();

			// Exit code 0 == success, 1 == user cancelled
			if (process.ExitCode == 0 && !string.IsNullOrEmpty(output))
			{
				return output;
			}
			if (process.ExitCode == 1 && string.IsNullOrEmpty(output))
			{
				return "";
			}
			throw new Exception($"osascript error: {error}");
		}
		catch (Exception e)
		{
			Console.WriteLine($"Mac dialog failed: {e.Message}");
			return "";
		}
	}

	private static string Escape(string s) => s?.Replace("\"", "\\\"") ?? "";
}
