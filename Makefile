
setup:
	dotnet restore AverageAudioBook.AudioConverter.sln
cli:
	dotnet run --project AudioConverter.Cli
build:
	dotnet build --nologo --self-contained AverageAudioBook.AudioConverter.sln
