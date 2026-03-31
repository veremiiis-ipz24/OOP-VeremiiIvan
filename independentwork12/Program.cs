using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== IndependentWork12: PLINQ Research ===\n");

        RunTest(1_000_000);
        RunTest(5_000_000);
        RunTest(10_000_000);

        Console.WriteLine("\n=== Side effects demo ===\n");

        SideEffectProblem();
        SideEffectFixed();
    }

    static List<int> GenerateData(int size)
    {
        Random rand = new Random(1);
        List<int> data = new List<int>(size);

        for (int i = 0; i < size; i++)
            data.Add(rand.Next(1, 1_000_000));

        return data;
    }

    static bool IsPrime(int number)
    {
        if (number < 2) return false;

        int limit = (int)Math.Sqrt(number);

        for (int i = 2; i <= limit; i++)
        {
            if (number % i == 0)
                return false;
        }

        return true;
    }

    static void RunTest(int size)
    {
        Console.WriteLine($"--- {size:N0} elements ---");

        var data = GenerateData(size);

        Stopwatch sw = new Stopwatch();

        // LINQ
        sw.Start();

        var linqResult = data
            .Where(IsPrime)
            .Select(x => Math.Sqrt(x))
            .ToList();

        sw.Stop();

        Console.WriteLine($"LINQ time: {sw.ElapsedMilliseconds} ms");

        // PLINQ
        sw.Restart();

        var plinqResult = data
            .AsParallel()
            .Where(IsPrime)
            .Select(x => Math.Sqrt(x))
            .ToList();

        sw.Stop();

        Console.WriteLine($"PLINQ time: {sw.ElapsedMilliseconds} ms");
        Console.WriteLine($"Result count: {plinqResult.Count}\n");
    }

    static void SideEffectProblem()
    {
        Console.WriteLine("---- Side Effect Problem ----");

        var data = Enumerable.Range(1, 1_000_000);

        int sum = 0;

        data.AsParallel().ForAll(x =>
        {
            sum += x;
        });

        Console.WriteLine($"Incorrect sum: {sum}");
    }

    static void SideEffectFixed()
    {
        Console.WriteLine("---- Fixed Version (lock) ----");

        var data = Enumerable.Range(1, 1_000_000);

        int sum = 0;
        object locker = new object();

        data.AsParallel().ForAll(x =>
        {
            lock (locker)
            {
                sum += x;
            }
        });

        Console.WriteLine($"Correct sum: {sum}");
    }
}