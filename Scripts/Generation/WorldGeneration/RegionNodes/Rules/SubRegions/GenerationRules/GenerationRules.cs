using System;

namespace ProjectAlps.Generation.WorldGeneration.RegionNodes.Rules
{
    public class GenerationRules
    {
        public int MinExtent { get; set; }

        public int MaxExtent { get; set; }

        public ElevationRules Elevation { get; set; }
    }
}
