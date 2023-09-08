namespace CountWordsTests;

[Collection("FilesCollectionFixture")]
public class WordCountTests
{
    private readonly FilesFixture _filesFixture;

    public WordCountTests(FilesFixture filesFixture)
    {
        _filesFixture = filesFixture;
    }

    [Fact]
    public void WordCount_Main_WithValidFiles_ReturnsCorrectWordCounts()
    {
        var filePath1 = Path.Combine(FilesFixture.TestDirectory, "TestFile1.txt");
        var filePath2 = Path.Combine(FilesFixture.TestDirectory, "TestFile2.txt");

        var entryPoint = typeof(Program).Assembly.EntryPoint!;

        var result = CaptureConsoleOutput(() => entryPoint.Invoke(null, new object[] { new[] { filePath1, filePath2 } }));

        var lines = result.Split('\n');
        var expectedCounts = new Dictionary<string, int>
        {
            ["Go"] = 1,
            ["do"] = 2,
            ["that"] = 2,
            ["thing"] = 1,
            ["you"] = 1,
            ["so"] = 1,
            ["well"] = 2,
            ["I"] = 1,
            ["play"] = 1,
            ["football"] = 1,
        };

        foreach (var line in lines)
        {
            var parts = line.Split(':');
            if (parts.Length == 2)
            {
                var word = parts[0].Trim();
                var count = int.Parse(parts[1].Trim());
                Assert.True(expectedCounts.ContainsKey(word));
                Assert.Equal(expectedCounts[word], count);
            }
        }
    }

    private static string CaptureConsoleOutput(Action action)
    {
        var originalOut = Console.Out;
        using var writer = new StringWriter();
        Console.SetOut(writer);

        action.Invoke();

        Console.SetOut(originalOut);
        return writer.ToString();
    }
}
