using System;
using System.Collections.Generic;

namespace ProjectAlps.Generation.WorldGeneration.RegionNodes.Rules
{
    public class LinkRules
    {
        public int MinLinks { get; set; }

        public int MaxLinks { get; set; }

        public List<int> Neighbours { get; set; }
    }
}