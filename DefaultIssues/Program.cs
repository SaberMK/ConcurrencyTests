using System;

namespace DefaultIssues
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = string.Empty;

            do
            {
                Console.Clear();
                Console.WriteLine(Placeholder);
                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        NonAtomicOperations.Do();
                        break;

                    case "2":
                        Deadlock.Do();
                        break;

                    case "3":
                        NonAtomicLockSolution.Do();
                        break;

                    case "4":
                        SimplestLockForDebugging.Do();
                        break;

                    case "5":
                        DeadlockMonitor_TryEnter_Solution.Do();
                        break;

                    case "6":
                        WaitAndPulseExamples.Do();
                        break;
                }

                Console.WriteLine("To continue press ANY KEY (keep in mind that RESET/POWER does not count)");
                Console.ReadKey();

            } while (!string.IsNullOrWhiteSpace(input));
        }

        static string Placeholder = @"
Enter a number to run an example:
1 - NonAtomicOperations problem
2 - Deadlock problem(would die at some point)
3 - lock(...) example
4 - lock(put a breakpoint at check out Dissasembly window)
5 - Monitor.TryEnter(...) solution for acquiring lock in a span of time
6 - WaitAndPulseExamples  - examples of Monitor.Wait and Monitor.Pulse
";
    }
}
