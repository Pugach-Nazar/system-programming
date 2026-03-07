using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        Console.Write("Enter a number for factorial and sum calculation: ");
        int N = int.Parse(Console.ReadLine());

        Parallel.Invoke(
            () => Factorial(N),
            () => Sum(N),
            () => PrintMessage()
        );

        Console.WriteLine("All methods have completed.");
    }

    static void Factorial(int n)
    {
        long fact = 1;
        for (int i = 1; i <= n; i++)
            fact *= i;

        Console.WriteLine($"Factorial of {n} = {fact}");
    }

    static void Sum(int n)
    {
        int sum = 0;
        for (int i = 1; i <= n; i++)
            sum += i;

        Console.WriteLine($"Sum from 1 to {n} = {sum}");
    }

    static void PrintMessage()
    {
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine("Method 3 is running...");
            Thread.Sleep(300);
        }
    }
}