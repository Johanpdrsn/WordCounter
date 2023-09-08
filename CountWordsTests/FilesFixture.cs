namespace CountWordsTests;

public class FilesFixture : IDisposable
{
    public FilesFixture()
    {
        // Create a directory for test files
        Directory.CreateDirectory(TestDirectory);

        // Create test files
        File.WriteAllLines(Path.Combine(TestDirectory, "TestFile.txt"), new[]
        {
                "And this is a test.",
                "For a dot  and case and whitespace."
        });

        File.WriteAllLines(Path.Combine(TestDirectory, "TestFile1.txt"), new[]
        {
                "Go do that thing that you do so well"
        });

        File.WriteAllLines(Path.Combine(TestDirectory, "TestFile2.txt"), new[]
        {
                "I play football well"
        });
    }

    public void Dispose()
    {
        // Clean up test files and directory
        Directory.Delete(TestDirectory, true);
    }

    public static string TestDirectory => "TestFiles";
}

[CollectionDefinition("FilesCollectionFixture")]
public class WordCountTestCollection : ICollectionFixture<FilesFixture>
{
}