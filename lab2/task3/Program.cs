using System;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        Console.Write("Input N: ");
        int N = int.Parse(Console.ReadLine());

        Task<int> sumTask = Task.Run(() =>
        {
            int sum = 0;
            for (int i = 1; i <= N; i++)
                sum += i;
            return sum;
        });

        sumTask.ContinueWith(t =>
        {
            Console.WriteLine($"The sum from 1 to {N} = {t.Result}");
        });

        Console.ReadLine();
    }
}