using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DefaultIssues
{
    public static class DeadlockMonitor_TryEnter_Solution
    {
        /// <summary>
        /// The main purpose of this example is to check out how
        /// we can take a lock for a couple of independant objects and,
        /// somehow, avoid deadlocks
        /// </summary>
        public static void Do()
        {
            const int count = 1000;

            var a = new object();
            var b = new object();

            var thread1 = new Thread(
                () =>
                {
                    for (int i = 0; i < count; ++i)
                    {
                        lock (a)
                            lock (b)
                                Thread.SpinWait(100);

                        if (i % 100 == 0)
                            Console.WriteLine($"{i / 10} % completed.");
                    }

                });

            var thread2 = new Thread(() => LockTimeout(a, b, count));

            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();

            Console.WriteLine("Done");
        }

        public static void LockTimeout(object a, object b, int count)
        {
            bool acquiredA = false;
            bool acquiredB = false;

            const int waitSeconds = 5;
            const int retryCount = 3;

            for(int i =0;i<count;++i)
            {
                int retries = 0;
                while(retries < retryCount)
                {
                    try
                    {
                        acquiredB = Monitor.TryEnter(b, TimeSpan.FromSeconds(waitSeconds));
                        if(acquiredB)
                        {
                            try
                            {
                                acquiredA = Monitor.TryEnter(a, TimeSpan.FromSeconds(waitSeconds));
                                if (acquiredA)
                                {
                                    Thread.SpinWait(100);
                                    break;
                                }
                                else
                                {
                                    retries++;
                                }
                            }
                            finally
                            {
                                if (acquiredA)
                                    Monitor.Exit(a);
                            }
                        }
                        else
                        {
                            retries++;
                        }
                    }
                    finally
                    {
                        if (acquiredB)
                            Monitor.Exit(b);
                    }
                }

                if (retries >= retryCount)
                    Console.WriteLine("could not obtain locks");
            }
        }
    }
}
