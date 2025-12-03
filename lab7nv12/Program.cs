﻿using System;
using System.IO;
using System.Net.Http;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine(" LAB 7 — Retry Pattern / IO & Network Errors \n");

        var fileProcessor = new FileProcessor();
        var networkClient = new NetworkClient();

        string reportPath = "report.txt";
        string reportContent = "This is a test report.";
        
        // Функція для визначення, чи варто повторювати спробу
        bool ShouldRetry(Exception ex) =>
            ex is IOException || ex is HttpRequestException;

        Console.WriteLine("=== Saving report with retry ===");

        RetryHelper.ExecuteWithRetry(
            () => {
                fileProcessor.SaveReport(reportPath, reportContent);
                return true;
            },
            retryCount: 5,
            initialDelay: TimeSpan.FromSeconds(1),
            shouldRetry: ShouldRetry
        );

        Console.WriteLine("\nUploading report with retry ");

        RetryHelper.ExecuteWithRetry(
            () => networkClient.UploadReport("https://server/upload", reportContent),
            retryCount: 4,
            initialDelay: TimeSpan.FromSeconds(1),
            shouldRetry: ShouldRetry
        );

        Console.WriteLine("\n End of program ");
    }
}
