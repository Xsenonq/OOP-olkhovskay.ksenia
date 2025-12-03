using System;
using System.Threading;

public static class RetryHelper
{
    public static T ExecuteWithRetry<T>(
        Func<T> operation,
        int retryCount = 3,
        TimeSpan initialDelay = default,
        Func<Exception, bool> shouldRetry = null)
    {
        if (initialDelay == default)
            initialDelay = TimeSpan.FromSeconds(1);

        for (int attempt = 1; attempt <= retryCount + 1; attempt++)
        {
            try
            {
                Console.WriteLine($"[RetryHelper] Attempt {attempt}/{retryCount + 1}");
                return operation();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[RetryHelper] Error: {ex.Message}");

                bool retry = shouldRetry == null || shouldRetry(ex);

                if (!retry)
                {
                    Console.WriteLine("[RetryHelper] Exception type not eligible for retry.");
                    throw;
                }

                if (attempt > retryCount)
                {
                    Console.WriteLine("[RetryHelper] Max retry attempts reached.");
                    throw;
                }

                TimeSpan delay = TimeSpan.FromMilliseconds(initialDelay.TotalMilliseconds * Math.Pow(2, attempt - 1));
                Console.WriteLine($"[RetryHelper] Waiting {delay.TotalSeconds}s before next attempt...");
                Thread.Sleep(delay);
            }
        }

        return default;
    }
}
