using System;

namespace ProjectAlps.Generation.WorldGeneration.WorldArchetype
{
    public class ConnettivityRules
    {
        public int MinLinksPerNode { get; set; }

        public int MaxLinksPerNode { get; set; }

        public float BranchingPreferences { get; set; }

        public bool AllowLoops { get; set; }
    }
}