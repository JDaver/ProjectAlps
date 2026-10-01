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
    Dictionary<int, int> regionOccurrences,
    int maxTargetDistance)
{

    var candidates = RulesByAltitude.Altitudes
        .Where(altitude =>
            Math.Abs(altitude - targetAltitude) <= maxTargetDistance)
        .SelectMany(altitude =>
            RulesByAltitude.Get(altitude))
        .Where(id =>
        {
            int occurrences =
                regionOccurrences.GetValueOrDefault(id, 0);

            int maxOccurrences =
                RegionTypes[id].OccurrenceRules.Max;

            return occurrences < maxOccurrences;
        })
        .ToList();

    if (candidates.Count > 0)
    {
        int selectedId = candidates
            .OrderBy(id =>
                regionOccurrences.GetValueOrDefault(id, 0))
            .ThenBy(id =>
                Math.Abs(
                    RegionTypes[id]
                        .GenerationRules
                        .Elevation
                        .Mean
                    - targetAltitude))
            .First();

        return RegionTypes[selectedId];
    }

    var fallbackCandidates = RegionTypes
        .Where(pair =>
        {
            int id = pair.Key;

            int occurrences =
                regionOccurrences.GetValueOrDefault(id, 0);

            int maxOccurrences =
                pair.Value.OccurrenceRules.Max;

            return occurrences < maxOccurrences;
        })
        .Select(pair => pair.Key)
        .ToList();

    if (fallbackCandidates.Count == 0)
    {
        throw new InvalidOperationException(
            "Nessuna SubRegion disponibile: tutte hanno raggiunto OccurrenceRules.Max."
        );
    }

    int fallbackId = fallbackCandidates
        .OrderBy(id =>
            Math.Abs(
                RegionTypes[id]
                    .GenerationRules
                    .Elevation
                    .Mean
                - targetAltitude))
        .ThenBy(id =>
            regionOccurrences.GetValueOrDefault(id, 0))
        .First();

    return RegionTypes[fallbackId];
}
}