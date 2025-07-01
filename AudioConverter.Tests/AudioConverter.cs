using AverageAudioBook.AudioConverter;

namespace AudioConverter.Tests;

public class AudioConverterTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void SuffixChanged()
    {
        string[] inputs = ["/folder/folder/audio.mp4", "/folder/directory/audio2.m4b", "audio3.mp3", "noextension"];
        string[] expected = ["/folder/folder/audio.mp3", "", "audio3.mp4", ""];
        int i = 0;
        foreach (string input in inputs)
        {
            string suffixChange = Utilities.ChangeSuffix(inputFile: input);
            Assert.That(suffixChange, Is.EqualTo(expected[i]));
            i++;
        }
    }

    [Test]
    public void DirectoryExists()
    {
        string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        bool isDirectory = Utilities.VerifyDirectory(inputDirectory: homeDirectory);
        Assert.That(isDirectory, Is.EqualTo(true));
    }
}
