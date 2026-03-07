using System;
using System.Text;
using System.Threading;

class Program
{
    static void Main()
    {
        // Створення першого потоку (числа)
        Thread numbersThread = new Thread(PrintNumbers);
        numbersThread.Name = "NumbersThread";

        // Створення другого потоку (літери)
        Thread lettersThread = new Thread(PrintLetters);
        lettersThread.Name = "LettersThread";

        // Запуск потоків
        numbersThread.Start();
        lettersThread.Start();

        // Очікування завершення потоків
        numbersThread.Join();
        lettersThread.Join();

        Console.WriteLine("Thread execution has completed.");
    }

    static void PrintNumbers()
    {
        for (int i = 1; i <= 40; i++)
        {
            Console.WriteLine($"{Thread.CurrentThread.Name}: {i}");
            Thread.Sleep(200);
        }
    }

    static void PrintLetters()
    {
        for (char c = 'A'; c <= 'Z'; c++)
        {
            Console.WriteLine($"{Thread.CurrentThread.Name}: {c}");
            Thread.Sleep(300);
        }
    }
}