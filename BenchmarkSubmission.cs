using System.Collections.Generic;

namespace MicrobenchmarkGui
{
    public class BenchmarkSubmission
    {
        public string TestName { get; set; }
        public string CpuName { get; set; }
        public string MotherboardName { get; set; }
        public string MemoryConfig { get; set; }
        public string Notes { get; set; }
        public float[][] Results { get; set; }

        public BenchmarkSubmission()
        {
            Results = new float[0][];
        }
    }
} 