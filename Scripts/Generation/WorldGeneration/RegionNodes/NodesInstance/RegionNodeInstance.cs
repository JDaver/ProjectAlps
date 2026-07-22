using System;
using System.Collections.Generic;
using ProjectAlps.Generation.WorldGeneration.RegionNodes.Rules;
using ProjectAlps.Generation.WorldGeneration.WorldArchetype.Instance;

namespace ProjectAlps.Generation.WorldGeneration.RegionNodes.RegionNodesInstance;

    public class RegionNodesInstance
    {
        public SubRegionRules[] RegionsCollection {get; private set; }

        public GenerateNodesCollection(int seed,ArchetypeInstance ArchetypeInstance, NodeLoader loader){
            int numberOfRegions = ArchetypeInstance.NumberRegions;
            string elevationProfile = ArchetypeInstance.Type;

            
        }
    }
