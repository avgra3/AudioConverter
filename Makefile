NOLOGO = --nologo

test:
	dotnet test $(NOLOGO) 
setup:
	dotnet restore $(NOLOGO) AverageAudioBook.AudioConverter.sln
cli:
	dotnet run $(NOLOGO) --project AudioConverter.Cli
build: test
	dotnet build $(NOLOGO) --output=./build/debug --verbosity=d  AudioConverter.Cli/
release: clean test
	dotnet publish $(NOLOGO) --output=./build/release AudioConverter.Cli/
clean:
	rm -rf build/
run: test
	dotnet run --project AudioConverter.Cli
