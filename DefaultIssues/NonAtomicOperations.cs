using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DefaultIssues
{
    public static class NonAtomicOperations
    {
        public static void Do()
        {
            const int iterations = 10000;
            var counter = 0;
            ThreadStart proc = () =>
            {
                for (var i = 0; i < iterations; ++i)
                {
                    counter++;
                    Thread.SpinWait(100);
                    counter--;
                }
            };

            var threads = Enumerable
                .Range(0, 8)
                .Select(n => new Thread(proc))
                .ToArray();

            foreach (var thread in threads)
                thread.Start();

            foreach (var thread in threads)
                thread.Join();

            Console.WriteLine($"Counter is: {counter}");
        }
    }
}
