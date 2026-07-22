using System;
namespace ProjectAlps.Generation.WorldGeneration.WorldArchetype.Rules;

    public class WorldArchetypeRules
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ConnettivityRules ConnettivityRules { get; set; }

        public ElevationProfile ElevationProfile { get; set; }

        public RegionRules RegionRules { get; set;}
    }
