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

        private void EvaluateAltitudeProfile(int seed,ElevationProfile currentElevationArchetype, int numberOfRegions)
        {
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
            Dictionary<int, int> regionOccurrences = new Dictionary<int, int>();
            int maxTargetDistance = 400;

            for (int i = 0; i < ElevationTargets.Length; i++)
            {
                int target = ElevationTargets[i];

                // Find RegionType with closest mean altitude to target
                int closestKey = loader.RegionTypes.Keys
                    .MinBy(k => Math.Abs(k - target));

                SubRegionRules rules = loader.RegionTypes[closestKey];
                int candidateId = rules.Id;

                // Find alternatives if already used in range of maxTargetDistance
                if (regionOccurrences.ContainsKey(candidateId))
                {
                    var candidates = loader.RegionTypes.Keys
                        .Where(k =>
                            Math.Abs(k - target) <= maxTargetDistance)
                        .OrderBy(k =>
                            regionOccurrences.GetValueOrDefault(loader.RegionTypes[k].Id, 0))
                        .ThenBy(k =>
                            Math.Abs(k - target));

                    int? selectedKey = candidates.FirstOrDefault();

                    // if a suitable alternative is found, use it; otherwise, keep the original candidate
                    if (selectedKey != null && loader.RegionTypes.ContainsKey(selectedKey.Value))
                    {
                        rules = loader.RegionTypes[selectedKey.Value];
                    }
                }

                RegionNode candidate = new RegionNode(
                    rules.Id,
                    rules.Name
                );

                RegionsCollection.Add(i, candidate);

                // Counter occurences update
                if (regionOccurrences.ContainsKey(candidate.Id))
                {
                    regionOccurrences[candidate.Id]++;
                }
                else
                {
                    regionOccurrences.Add(candidate.Id, 1);
                }
            }
        }
    }
