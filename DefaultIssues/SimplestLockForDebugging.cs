using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultIssues
{
    public static class SimplestLockForDebugging
    {
        /// <summary>
        /// The idea of this example is to debug this method with ILDASM
        /// and check out how it looks like in dissasembly window
        /// </summary>
        public static void Do()
        {
            var ctr = 0;
            var lockFlag = new object();
            lock (lockFlag)
                ctr++;
        }
    }
}
