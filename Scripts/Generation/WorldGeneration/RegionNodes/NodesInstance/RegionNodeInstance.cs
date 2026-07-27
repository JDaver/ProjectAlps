using System;
using System.Collections.Generic;
using System.Linq;
using ProjectAlps.Generation.WorldGeneration.RegionNodes;
using ProjectAlps.Generation.WorldGeneration.RegionNodes.Rules;
using ProjectAlps.Generation.WorldGeneration.WorldArchetype.Instance;
using ProjectAlps.Generation.WorldGeneration.WorldArchetype.Rules;
using ProjectAlps.Functions;

namespace ProjectAlps.Generation.WorldGeneration.RegionNodes;

    public class RegionNodesInstance
    {
        public Dictionary<int,RegionNode> RegionsCollection {get; private set; } = new Dictionary<int, RegionNode>(); 
        private int[] ElevationTargets;

        public RegionNodesInstance(int seed,ArchetypeInstance ArchetypeInstance, NodeLoader loader){
            int numberOfRegions = ArchetypeInstance.NumberRegions;
            ElevationProfile ElevationArchetype = ArchetypeInstance.ElevationProfile;
        
            EvaluateAltitudeProfile(seed,ElevationArchetype,numberOfRegions);
            GenerateNodesCollection(loader);
        }

        private void EvaluateAltitudeProfile(int seed,ElevationProfile currentElevationArchetype, int numberOfRegions){
            Random rng = new Random(seed);
            string function = currentElevationArchetype.Function;
            int minRange = currentElevationArchetype.Min;
            int maxRange = currentElevationArchetype.Max;
            ElevationTargets = new int[numberOfRegions];
            float steps = 1f / (numberOfRegions - 1) ;
            float currentStep = 0f;

            for (int i = 0; i < numberOfRegions; i++){
                float noise = ((float)rng.NextDouble() * 0.2f) - 0.1f;
                float x =  MathF.Abs(currentStep + noise);
                float y = MathF.Abs( FunctionRegistry.Evaluate(function,x));
               
                ElevationTargets[i] = (int)(minRange +  y * (maxRange - minRange));
                currentStep = currentStep + steps;
            }
        }

        private void GenerateNodesCollection(NodeLoader loader)
{
    HashSet<int> usedRegionTypes = new HashSet<int>();

    for(int i = 0; i < ElevationTargets.Length; i++)
    {
        int target = ElevationTargets[i];

        int closestKey = loader.RegionTypes.Keys
            .MinBy(k => Math.Abs(k - target));

        SubRegionRules rules = loader.RegionTypes[closestKey];

        int candidateId = rules.Id;

        if(usedRegionTypes.Contains(candidateId) &&
           usedRegionTypes.Count < loader.RegionTypes.Count)
        {
            candidateId = loader.RegionTypes.Keys
                .Where(k => !usedRegionTypes.Contains(loader.RegionTypes[k].Id))
                .MinBy(k => Math.Abs(k - target));

            rules = loader.RegionTypes[candidateId];
        }


        RegionNode candidate = new RegionNode(
            rules.Id,
            rules.Name
        );

        RegionsCollection.Add(i, candidate);
        usedRegionTypes.Add(candidate.Id);
    }
}
    }
