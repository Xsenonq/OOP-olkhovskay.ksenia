using System;
using System.IO;

public class FileProcessor
{
    private int _attempts = 0;

    // Імітація збереження файлу 
    public void SaveReport(string path, string reportContent)
    {
        _attempts++;
        Console.WriteLine($"[FileProcessor] Attempt {_attempts}: Saving report...");

        if (_attempts <= 3)
            throw new IOException("Disk I/O error while saving report!");

        Console.WriteLine("[FileProcessor] Report saved successfully!");
    }
}
