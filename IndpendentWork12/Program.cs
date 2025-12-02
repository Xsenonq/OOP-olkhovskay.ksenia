using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading;
using System.Collections.Concurrent;
using System.Text;
using System.Globalization; // Added for culture-invariant output

class Program
{
    // Settings (You can modify these for experiments)
    static readonly int[] SizesToTest = new[] { 1000000, 3000000, 5000000 };
    static readonly int RandomSeed = 12345;
    static readonly int MaxRandomValue = 10000000;
    static readonly int WarmupRuns = 1;

    static void Main(string[] args)
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

       
        Console.OutputEncoding = Encoding.UTF8;
        
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("IndependentWork12: PLINQ vs LINQ Performance Analysis ");
        Console.ResetColor();


        var results = new List<(int Size, double LinqTime, double PlinqTime)>();

        foreach (var n in SizesToTest)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n--- [Experiment] Collection Size {n:N0} ---");
            Console.ResetColor();
            
            var data = GenerateIntList(n, MaxRandomValue, RandomSeed);
            Console.WriteLine("[Warmup (JIT) in progress...]");
            for (int i = 0; i < WarmupRuns; i++)
            {
                var _ = RunLinq(data).Take(10).ToArray(); 
                var __ = RunPlinq(data).Take(10).ToArray();
            }
            Console.WriteLine("[Measuring performance...]");
            TimeSpan tLinq = MeasureElapsed(() => RunLinq(data).ToList());
            TimeSpan tPlinq = MeasureElapsed(() => RunPlinq(data).ToList());
            
            double linqSec = tLinq.TotalSeconds;
            double plinqSec = tPlinq.TotalSeconds;

            results.Add((n, linqSec, plinqSec));

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[LINQ Time]: {linqSec:F3} s");
            Console.WriteLine($"[PLINQ Time]: {plinqSec:F3} s");
            Console.ResetColor();

            if (plinqSec < linqSec)
            {
                double speedup = linqSec / plinqSec;
                Console.WriteLine($"[Result]: PLINQ is faster by {speedup:F2} times.");
            }
            else
            {
                 Console.WriteLine($"[Result]: LINQ proved to be faster.");
            }
        }
        
        Console.WriteLine();
        PrintResultsTable(results);
        Console.WriteLine("\n" + new string('=', 70));
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n--- PLINQ Side Effects Demonstration (Safety Investigation) ---");
        Console.WriteLine(new string('-', 70));
        Console.ResetColor();
        
        var smallData = GenerateIntList(1000000, 1000, RandomSeed + 1);

        Console.WriteLine("\n[1] Unsafe Example (Race Condition)");
        UnsafePlinqSideEffect(smallData);

        Console.WriteLine("\n[2] Fix #1: Interlocked (Atomic Operation)");
        FixedWithInterlocked(smallData);

        Console.WriteLine("\n[3] Fix #2: Concurrent Collection");
        FixedWithConcurrentCollection(smallData);

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(" Program finished. Data analysis is in the table. ");
        Console.ResetColor();
    }

    /// <summary>
    /// 
    /// </summary>
    static void PrintResultsTable(List<(int Size, double LinqTime, double PlinqTime)> results)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("--- Summary Table of Performance Results (Heavy Computation) ---");
        Console.ResetColor();

        Console.WriteLine($"{"Size (N)",-15} | {"LINQ Time (s)",-15} | {"PLINQ Time (s)",-15} | {"Speedup (x)",-10}");
        Console.WriteLine(new string('-', 70));

        foreach (var res in results)
        {
            double speedup = res.LinqTime / res.PlinqTime;
            string arrow = speedup > 1.0 ? ">>" : "<<"; 

            
            Console.WriteLine(
                $"{res.Size:N0,-15} | {res.LinqTime:F3,-15} | {res.PlinqTime:F3,-15} | {speedup:F2}x {arrow,-7}"
            );
        }
    }

    static List<int> GenerateIntList(int count, int maxValueExclusive, int seed)
    {
        var rnd = new Random(seed);
        var list = new List<int>(count);
        for (int i = 0; i < count; i++)
        {
            list.Add(rnd.Next(maxValueExclusive));
        }
        return list;
    }

    static double HeavyComputation(int x)
    {
        bool prime = IsPrimeDeterministic(x);
        double v = x;
        v = Math.Sqrt(v + 1) * (prime ? 1.00037 : 0.99963);
        v = Math.Log(v + 1.0) + Math.Sin(v);
        return v;
    }

    static bool IsPrimeDeterministic(int n)
    {
        if (n < 2) return false;
        if (n % 2 == 0) return n == 2;
        int r = (int)Math.Sqrt(n);
        for (int i = 3; i <= r; i += 2)
            if (n % i == 0) return false;
        return true;
    }

    static IEnumerable<double> RunLinq(IEnumerable<int> source)
    {
        return source.Where(x => x > 1).Select(x => HeavyComputation(x));
    }

    static IEnumerable<double> RunPlinq(IEnumerable<int> source)
    {
        return source.AsParallel().Where(x => x > 1).Select(x => HeavyComputation(x));
    }

    static TimeSpan MeasureElapsed(Action action)
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        var sw = Stopwatch.StartNew();
        action();
        sw.Stop();
        return sw.Elapsed;
    }

    static void UnsafePlinqSideEffect(IEnumerable<int> source)
    {
        long sharedCounter = 0;
        source.AsParallel().ForAll(x =>
        {
            if (x % 2 == 0)
            {
                sharedCounter += 1; 
            }
        });

        long expected = source.Count(x => x % 2 == 0);
        Console.WriteLine($"Expected (even count): {expected}");
        Console.WriteLine($"Received (no sync): {sharedCounter} [INCORRECT]");
        Console.WriteLine("*Comment: The received value is incorrect due to a Race Condition.*");
    }

    static void FixedWithInterlocked(IEnumerable<int> source)
    {
        long safeCounter = 0;
        source.AsParallel().ForAll(x =>
        {
            if (x % 2 == 0)
            {
                Interlocked.Increment(ref safeCounter);
            }
        });

        long expected = source.Count(x => x % 2 == 0);
        Console.WriteLine($"Expected: {expected}");
        Console.WriteLine($"Received (Interlocked): {safeCounter} [CORRECT]");
    }

    static void FixedWithConcurrentCollection(IEnumerable<int> source)
    {
        var bag = new ConcurrentBag<int>();
        source.AsParallel().ForAll(x =>
        {
            if (x % 2 == 0)
            {
                bag.Add(1);
            }
        });
        int result = bag.Count;
        int expected = source.Count(x => x % 2 == 0);
        Console.WriteLine($"Expected: {expected}");
        Console.WriteLine($"Received (ConcurrentBag): {result} [CORRECT]");
    }
}