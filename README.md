# AudioConverter

## Purpose

A [library](./AudioConverter) and [TUI](./AudioConverter.Cli) to convert audiobooks from Audible (and to add in the future other formats) to M4B (and to other versions in the future).

## Prerequisites

You must have [FFMPEG](https://ffmpeg.org/) installed and accessible.

For Audible conversion, you must have access to your activation bytes which are tied to your Audible account. It is reccomended to add your activation bytes to your environment variables. If on Linux use the below command from the terminal _or_ for permenance, add it to your configuration file (.bashrc, .zshrc, etc.) usually located in your home directory.

```bash
export ACTIVATION_BYTES="########"
```

If you are building from source, you must also have a compatible version of the [.NET](https://dotnet.microsoft.com/en-us/) sdk installed on your machine.


### Building from Source

Using the [Makefile](./Makefile), run the following in your terminal:

```bash
# Debug version
make build

# DEFAULTS to building in build/debug

# Release version
make release
# Defualts to building in build/release
```

## External Dependencies

- [CliWrap](https://github.com/Tyrrrz/CliWrap): Allows us to make an easy wrapper to ffmpeg
- [Spectre.Console](https://spectreconsole.net/): TUI dependency

## Progress/Desired Features

- [ ] Ensure ffmpeg is installed before building.
    - Validated only once the process has started due to the option for using ffmpeg which is not on the user's path.
- [x] Make TUI
- [x] Allow for path variables for activation bytes and ffmpeg.
- [ ] Add other conversion options (mp3 to mp4, etc.)
- [x] Ask the user to determine what they would like to do if there is already a copy of the expected output file (overwrite or not)
- [ ] Show the user errors from ffmpeg to allow  them to determine what went wrong - and allow for easier debug
- [ ] Add some tests for some of the methods
    - [ ] Unit tests
    - [ ] Integration tests
- [ ] Allow for change of default Output location
- [ ] Once conversion - notify user where the output location is
- [ ] _Optional:_ Allow the user to open their system's default file explorer
    - [ ] Linux
    - [ ] Windows
    - [ ] OSX
