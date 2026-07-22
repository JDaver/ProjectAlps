using System;
using Distribution;

namespace ProjectAlps.Generation.WorldGeneration.RegionNodes.Rules
{
    public enum DistributionType
    {
        Normal,
        Gamma,
        Uniform
    }

    public class ElevationRules
    {
        public float Mean { get; set; }                 // μ

        public float StandardDeviation { get; set; }    // σ
        
        public DistributionType Distribution { get; set; }
    }
}

