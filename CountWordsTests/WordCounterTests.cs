using System.Collections.Concurrent;
using CountWords;

namespace CountWordsTests;

[Collection("FilesCollectionFixture")]
public class WordCounterTests
{
    [Fact]
    public async void Throws_When_File_Doesnt_Exist()
    {
        await Assert.ThrowsAsync<WordCounterException>(async () => await WordCounter.CountWordsInFile("notafile", new ConcurrentDictionary<string, int>()));
    }

    [Fact]
    public async Task WordCount_ProcessFileAsync_CountsWordsCorrectly()
    {
        var filePath = Path.Combine(FilesFixture.TestDirectory, "TestFile.txt");
        var wordCounts = new ConcurrentDictionary<string, int>();

        await WordCounter.CountWordsInFile(filePath, wordCounts);

        Assert.Equal(1, wordCounts["And"]);
        Assert.Equal(1, wordCounts["this"]);
        Assert.Equal(1, wordCounts["is"]);
        Assert.Equal(2, wordCounts["a"]);
        Assert.Equal(1, wordCounts["test"]);
        Assert.Equal(1, wordCounts["For"]);
        Assert.Equal(1, wordCounts["dot"]);
        Assert.Equal(2, wordCounts["and"]);
        Assert.Equal(1, wordCounts["case"]);
        Assert.Equal(1, wordCounts["whitespace"]);
    }
}
