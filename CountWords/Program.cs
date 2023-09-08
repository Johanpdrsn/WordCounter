using System.Collections.Concurrent;
using CountWords;

if (args.Length == 0)
{
    Console.WriteLine("Provide 1 or more files");
}

var wordCounts = new ConcurrentDictionary<string, int>();

var tasks = args.Select(path => WordCounter.CountWordsInFile(path, wordCounts));

await Task.WhenAll(tasks);

foreach (var (key, val) in wordCounts)
{
    Console.WriteLine($"{key}: {val}");
}
