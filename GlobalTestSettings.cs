using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrobenchmarkGui
{
    /// <summary>
    /// Container class for settings that should apply across tests
    /// </summary>
    public static class GlobalTestSettings
    {
        /// <summary>
        /// Minimum test size in KB, for tests that go through multiple sizes
        /// </summary>
        public static uint MinTestSizeKb = 0;
    }
}
