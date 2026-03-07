using System;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;

class Program
{
    static object consoleLock = new object();
    static List<ThreadResult> results = new List<ThreadResult>();
    static long maxCount;

    static void Main()
    {
        Console.Write("Enter number of threads: ");
        int threadCount = int.Parse(Console.ReadLine());

        Console.Write("Enter max count value (e.g. 100000000): ");
        maxCount = long.Parse(Console.ReadLine());

        List<Thread> threads = new List<Thread>();

        for (int i = 0; i < threadCount; i++)
        {
            Console.WriteLine($"\nSelect priority for Thread {i + 1}:");
            Console.WriteLine("1 - Lowest");
            Console.WriteLine("2 - BelowNormal");
            Console.WriteLine("3 - Normal");
            Console.WriteLine("4 - AboveNormal");
            Console.WriteLine("5 - Highest");

            int choice = int.Parse(Console.ReadLine());

            ThreadPriority priority = choice switch
            {
                1 => ThreadPriority.Lowest,
                2 => ThreadPriority.BelowNormal,
                4 => ThreadPriority.AboveNormal,
                5 => ThreadPriority.Highest,
                _ => ThreadPriority.Normal
            };

            Thread t = new Thread(CountTask);
            t.Name = $"Thread {i + 1}";
            t.Priority = priority;

            threads.Add(t);
        }

        Stopwatch totalTime = Stopwatch.StartNew();

        foreach (var t in threads)
            t.Start();

        foreach (var t in threads)
            t.Join();

        totalTime.Stop();

        PrintResults();
    }

    static void CountTask()
    {
        Stopwatch sw = Stopwatch.StartNew();
        long counter = 0;

        while (counter < maxCount)
        {
            counter++;

            // Show progress every 10%
            if (counter % (maxCount / 10) == 0)
            {
                lock (consoleLock)
                {
                    Console.WriteLine($"{Thread.CurrentThread.Name} | " +
                                      $"Priority: {Thread.CurrentThread.Priority} | " +
                                      $"Progress: {(counter * 100) / maxCount}%");
                }
            }
        }

        sw.Stop();

        lock (results)
        {
            results.Add(new ThreadResult
            {
                Name = Thread.CurrentThread.Name,
                Priority = Thread.CurrentThread.Priority,
                TimeMs = sw.ElapsedMilliseconds
            });
        }
    }

    static void PrintResults()
    {
        double totalTime = 0;
        foreach (var r in results)
            totalTime += r.TimeMs;

        Console.WriteLine("Name\t\tPriority\tTime(ms)\tCPU %");
        Console.WriteLine("------------------------------------------------------");

        foreach (var r in results)
        {
            double percent = (r.TimeMs / totalTime) * 100;

            Console.WriteLine($"{r.Name}\t{r.Priority}\t{r.TimeMs}\t\t{percent:F2}%");
        }
    }
}

class ThreadResult
{
    public string Name;
    public ThreadPriority Priority;
    public long TimeMs;
}