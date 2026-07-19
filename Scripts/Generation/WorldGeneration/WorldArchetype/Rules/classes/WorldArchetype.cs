using System;
// using ProjectAlps.Generation.WorldGeneration.RegionGraph;

namespace ProjectAlps.Generation.WorldGeneration.WorldArchetype
{
    public class WorldArchetype
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ConnettivityRules ConnettivityRules { get; set; }

        public ElevationProfile ElevationProfile { get; set; }

        public RegionRules RegionRules { get; set;}
    }
}