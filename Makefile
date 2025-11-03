NOLOGO = --nologo

setup:
	dotnet restore $(NOLOGO) AverageAudioBook.AudioConverter.sln
cli:
	dotnet run $(NOLOGO) --project AudioConverter.Cli
build: test
	dotnet build $(NOLOGO) --output=./build/debug --verbosity=d AverageAudioBook.AudioConverter.sln

release: test
	dotnet publish $(NOLOGO) --output=./build/release AverageAudioBook.AudioConverter.sln

test:
	dotnet test $(NOLOGO) 
