using System;
using System.Net.Http;

public class NetworkClient
{
    private int _attempts = 0;

    // Імітація завантаження звіту (2 помилки, потім успіх)
    public bool UploadReport(string url, string reportContent)
    {
        _attempts++;
        Console.WriteLine($"[NetworkClient] Attempt {_attempts}: Uploading report to server...");

        if (_attempts <= 2)
            throw new HttpRequestException("Temporary network error!");

        Console.WriteLine("[NetworkClient] Report uploaded successfully!");
        return true;
    }
}
