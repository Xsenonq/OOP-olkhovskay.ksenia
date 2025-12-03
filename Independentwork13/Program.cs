using Polly;
using Polly.CircuitBreaker;
using Polly.Timeout;
using System;
using System.Net.Http;
using System.Threading;

namespace IndependentWork13
{
    public class Program
    {
        private static int _apiCallAttempts = 0;

        private static int _dbAttempts = 0;

        private static int _longOperationAttempts = 0;

        public static string CallExternalApi()
        {
            _apiCallAttempts++;
            Console.WriteLine($"Attempt {_apiCallAttempts}: Calling external API...");

            if (_apiCallAttempts <= 2)
                throw new HttpRequestException("External API temporary failure!");

            return "API Data";
        }

        public static string QueryDatabase()
        {
            _dbAttempts++;
            Console.WriteLine($"DB attempt {_dbAttempts}: Querying DB...");

            if (_dbAttempts <= 2)
                throw new Exception("Database connection lost!");

            return "DB response";
        }

        public static string LongOperation()
        {
            _longOperationAttempts++;
            Console.WriteLine($"Operation attempt {_longOperationAttempts}: Executing long task...");

            Thread.Sleep(4000); 

            return "Long operation completed";
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("\n===== Scenario 1: External API with Retry =====\n");

            var retryPolicy = Policy
                .Handle<HttpRequestException>()
                .WaitAndRetry(
                    3,
                    retry => TimeSpan.FromSeconds(Math.Pow(2, retry)),
                    (ex, ts, count, ctx) =>
                    {
                        Console.WriteLine($"Retry {count} after {ts.TotalSeconds}s: {ex.Message}");
                    });

            try
            {
                string result = retryPolicy.Execute(CallExternalApi);
                Console.WriteLine($"Final API result: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"API failed after retries: {ex.Message}");
            }

            Console.WriteLine("\n===== Scenario 2: Database with Retry + Circuit Breaker =====\n");

            var dbRetryPolicy = Policy
                .Handle<Exception>()
                .Retry(2, (ex, count) =>
                {
                    Console.WriteLine($"Retry DB {count}: {ex.Message}");
                });

            var breakerPolicy = Policy
                .Handle<Exception>()
                .CircuitBreaker(
                    2,
                    TimeSpan.FromSeconds(5),
                    onBreak: (ex, ts) =>
                    {
                        Console.WriteLine($"Circuit opened for {ts.TotalSeconds}s: {ex.Message}");
                    },
                    onReset: () =>
                    {
                        Console.WriteLine("Circuit reset.");
                    });

            try
            {
                string dbResult = Policy.Wrap(dbRetryPolicy, breakerPolicy).Execute(QueryDatabase);
                Console.WriteLine($"DB Result: {dbResult}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DB error after policies: {ex.Message}");
            }

            Console.WriteLine("\n===== Scenario 3: Timeout + Fallback =====\n");

            var timeoutPolicy = Policy
                .Timeout(2, TimeoutStrategy.Pessimistic,
                onTimeout: (ctx, ts, task) =>
                {
                    Console.WriteLine($"Timeout after {ts.Seconds}s!");
                });

            var fallbackPolicy = Policy<string>
                .Handle<TimeoutRejectedException>()
                .Fallback(() =>
                {
                    Console.WriteLine("Fallback activated!");
                    return "DEFAULT RESULT (Fallback)";
                });
            var combined = fallbackPolicy.Wrap(timeoutPolicy);
            string longResult = combined.Execute(LongOperation);
            Console.WriteLine($"Result: {longResult}");
            Console.WriteLine("\n===== END OF REPORT =====");
        }
    }
}
