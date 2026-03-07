using System;
using System.Threading;

class Program
{
    static void Main()
    {
        // First foreground thread
        Thread thread1 = new Thread(DoWork);
        thread1.Name = "Thread 1";

        // Second foreground thread
        Thread thread2 = new Thread(DoWork);
        thread2.Name = "Thread 2";

        // Background thread
        Thread backgroundThread = new Thread(BackgroundWork);
        backgroundThread.Name = "Background Thread";
        //backgroundThread.IsBackground = true;

        // Start threads
        thread1.Start();
        thread2.Start();
        backgroundThread.Start();

        //thread1.Join();
        //thread2.Join();

        Console.WriteLine("Main thread has finished execution.");
    }

    static void DoWork()
    {
        for (int i = 1; i <= 5; i++)
        {
            Console.WriteLine($"{Thread.CurrentThread.Name} is working... {i}");
            Thread.Sleep(500);
        }

        Console.WriteLine($"{Thread.CurrentThread.Name} has finished work.");
    }

    static void BackgroundWork()
    {
        while (true)
        {
            Console.WriteLine($"{Thread.CurrentThread.Name} is running in the background...");
            Thread.Sleep(1000);
        }
    }
}