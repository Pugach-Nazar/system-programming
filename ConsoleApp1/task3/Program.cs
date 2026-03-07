using System;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;

class Program
{
    const long MaxCount = 100_000_000;
    static List<ThreadResult> results = new List<ThreadResult>();

    static void Main()
    {
        ThreadPriority[] priorities =
        [
            ThreadPriority.Normal,
            ThreadPriority.AboveNormal,
            ThreadPriority.Highest
        ];

        List<Thread> threads = new List<Thread>();

        for (int i = 0; i < priorities.Length; i++)
        {

            Thread thread = new Thread(() => CountTask());
            thread.Name = $"Thread {i + 1}";
            thread.Priority = priorities[i];

            threads.Add(thread);
        }

        Stopwatch totalTime = Stopwatch.StartNew();

        foreach (var t in threads)
            t.Start();

        foreach (var t in threads)
            t.Join();

        totalTime.Stop();

        Console.WriteLine("\nExecution completed.\n");

        double totalMilliseconds = 0;
        foreach (var r in results)
            totalMilliseconds += r.ElapsedMilliseconds;

        foreach (var r in results)
        {
            double percent = (r.ElapsedMilliseconds / totalMilliseconds) * 100;

            Console.WriteLine($"{r.Name}");
            Console.WriteLine($"Priority: {r.Priority}");
            Console.WriteLine($"Iterations: {r.Iterations}");
            Console.WriteLine($"Time: {r.ElapsedMilliseconds} ms");
            Console.WriteLine($"CPU Share: {percent:F2}%\n");
        }
    }

    static void CountTask()
    {
        Stopwatch sw = Stopwatch.StartNew();

        long counter = 0;
        while (counter < MaxCount)
        {
            counter++;
        }

        sw.Stop();

        lock (results)
        {
            results.Add(new ThreadResult
            {
                Name = Thread.CurrentThread.Name,
                Priority = Thread.CurrentThread.Priority,
                Iterations = counter,
                ElapsedMilliseconds = sw.ElapsedMilliseconds
            });
        }
    }
}

class ThreadResult
{
    public string Name;
    public ThreadPriority Priority;
    public long Iterations;
    public long ElapsedMilliseconds;
}