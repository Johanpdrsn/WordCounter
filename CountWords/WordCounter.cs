using System.Collections.Concurrent;
using System.Text;

namespace CountWords;

public static class WordCounter
{
    public static async Task CountWordsInFile(string filePath, ConcurrentDictionary<string, int> wordCounts)
    {
        try
        {
            using var streamReader = new StreamReader(filePath, Encoding.UTF8);

            while ((await streamReader.ReadLineAsync()) is string line)
            {
                var words = line.Split(new[] { ' ', '\t', '.' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var word in words)
                {
                    wordCounts.AddOrUpdate(word, 1, (_, val) => val += 1);
                }
            }
        }
        catch (Exception ex)
        {
            throw new WordCounterException($"Error processing file '{filePath}': {ex.Message}");
        }
    }
}

public class WordCounterException : Exception
{
    public WordCounterException(string message) : base(message) { }
}
