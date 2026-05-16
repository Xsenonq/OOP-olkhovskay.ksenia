using lab29nv12.Services;

class Program
{
    static async Task Main(string[] args)
    {
        string input = "messages.csv";
        string output = "filtered.csv";

        int lines = 1_000_000;

        FileGenerator generator = new FileGenerator();
        FileProcessor processor = new FileProcessor();

        Console.WriteLine(" Генерація файлу...");
        await generator.GenerateAsync(input, lines);

        Console.WriteLine(" Асинхронна обробка...");
        var asyncResult = await processor.ProcessAsync(input, output);

        Console.WriteLine(" Синхронна обробка...");
        var syncResult = processor.ProcessSync(input);

        Console.WriteLine("\n РЕЗУЛЬТАТИ:");
        Console.WriteLine($"Async час: {asyncResult.Time} ms");
        Console.WriteLine($"Sync час: {syncResult.Time} ms");

        Console.WriteLine($"\nSpam: {asyncResult.SpamCount}");
        Console.WriteLine($"Normal: {asyncResult.NormalCount}");
    }
}
