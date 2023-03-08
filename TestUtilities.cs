using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrobenchmarkGui
{
    public static class TestUtilities
    {
        /// <summary>
        /// Scale iterations to reach target time.
        /// </summary>
        /// <param name="lastRunIterations">Last iteration count</param>
        /// <param name="targetTimeMs">Desired run time</param>
        /// <param name="lastTimeMs">Last run time</param>
        /// <returns></returns>
        public static ulong ScaleIterations(ulong lastRunIterations, float targetTimeMs, float lastTimeMs)
        {
            if (lastTimeMs < 100)
            {
                return lastRunIterations * 5;
            }

            return (ulong)(lastRunIterations * (targetTimeMs / lastTimeMs));
        }
    }
}
