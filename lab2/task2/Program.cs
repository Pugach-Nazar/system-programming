using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        Task t1 = Task.Run(() => Count());
        Task t2 = Task.Run(() => Count());
        Task t3 = Task.Run(() => Count());

        Task.WaitAll(t1, t2, t3);
    }

    static void Count()
    {
        for (int i = 1; i <= 5; i++)
        {
            Console.WriteLine($"Task Id: {Task.CurrentId}, Iteration: {i}");
            Thread.Sleep(200);
        }
    }
}