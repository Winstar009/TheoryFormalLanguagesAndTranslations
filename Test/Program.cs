using System;
using System.Collections.Generic;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            List<double> result = new List<double>();

            int a = -10, b = 10, h = 1;

            for (int i = a; i < b; i += h)
            {
                result.Add(4.1 + i);
            }

            Console.WriteLine("Result:\n");
            result.ForEach((item) =>
            {
                Console.WriteLine($"f(x) = {item}");
            });

            Console.ReadKey();
        }
    }
}
