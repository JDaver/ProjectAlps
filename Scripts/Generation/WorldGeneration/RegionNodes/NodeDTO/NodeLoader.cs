using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using ProjectAlps.Utils.PathManager;
using ProjectAlps.Generation.WorldGeneration.RegionNodes.Rules;

namespace ProjectAlps.Generation.WorldGeneration.RegionNodes;

public class NodeLoader
{
    public Dictionary<int, SubRegionRules> RegionTypes { get; private set; }

    public RulesByAltitude RulesByAltitude { get; private set; }


    public NodeLoader()
    {
        RegionTypes = new Dictionary<int, SubRegionRules>();
        RulesByAltitude = new RulesByAltitude();

        string json = File.ReadAllText(
            PathManager.RegionRules
        );

        SubRegionRulesDTO data =
            JsonSerializer.Deserialize<SubRegionRulesDTO>(json)
            ?? throw new Exception("Invalid RegionRules JSON");


        foreach(var node in data.SubRegions)
        {
            RegionTypes.Add(node.RegionTypeId, node);

            RulesByAltitude.Add(
                (int)node.GenerationRules.Elevation.Mean,
                node.RegionTypeId
            );
        }
    }


    public SubRegionRules FindClosestRule(
        int targetAltitude,
        Dictionary<int,int> regionOccurrences,
        int maxTargetDistance)
    {
        var candidates = RulesByAltitude.Altitudes
            .Where(altitude =>
                Math.Abs(altitude - targetAltitude) <= maxTargetDistance)
            .SelectMany(altitude =>
                RulesByAltitude.Get(altitude))
            .OrderBy(id =>
                regionOccurrences.GetValueOrDefault(id, 0))
            .ThenBy(id =>
                Math.Abs(
                    RegionTypes[id].GenerationRules.Elevation.Mean
                    - targetAltitude))
            .ToList();


        // fallback nel caso nessuna regola sia nel range
        if(candidates.Count == 0)
        {
            int closestAltitude = RulesByAltitude.Altitudes
                .MinBy(a => Math.Abs(a - targetAltitude));

            int closestId = RulesByAltitude
                .Get(closestAltitude)
                .First();

            return RegionTypes[closestId];
        }


        return RegionTypes[candidates.First()];
    }
}