using System;
using System.Collections.Generic;

namespace ProjectAlps.Generation.WorldGeneration.RegionNodes.Rules;
public class RulesByAltitude
{
    private Dictionary<int, List<int>> RulesIndex { get; } = new();

    public void Add(int altitude, int ruleId)
    {
        if (!RulesIndex.TryGetValue(altitude, out var rules))
        {
            rules = new List<int>();
            RulesIndex[altitude] = rules;
        }

        rules.Add(ruleId);
    }

    public List<int> Get(int altitude)
    {
        return RulesIndex[altitude];
    }
}