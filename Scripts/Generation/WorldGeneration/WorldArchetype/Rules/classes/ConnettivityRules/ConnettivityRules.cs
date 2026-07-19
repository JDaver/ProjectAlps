using System;
namespace ProjectAlps.Generation.WorldGeneration.WorldArchetype.Rules;
    public class ConnettivityRules
    {
        public int MinLinksPerNode { get; set; }

        public int MaxLinksPerNode { get; set; }

        public float BranchingPreference { get; set; }

        public bool AllowLoops { get; set; }
    }
