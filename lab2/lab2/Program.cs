using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        Task task1 = new Task(() =>
        {
            for (int i = 1; i <= 10; i++)
            {
                Console.WriteLine($"Number: {i}");
                Thread.Sleep(200);
            }
        });

        Task task2 = new Task(() =>
        {
            for (char c = 'A'; c <= 'J'; c++)
            {
                Console.WriteLine($"Letter: {c}");
                Thread.Sleep(200);
            }
        });

        task1.Start();
        task2.Start();

        Task.WaitAll(task1, task2);

        Console.WriteLine("Both tasks have completed.");
    }
}