using System;
using System.Collections.Generic;

namespace ProjectAlps.Generation.WorldGeneration.RegionNodes.Rules;
public class RulesByAltitude
{
    private readonly SortedDictionary<int, List<int>> rulesIndex = new();

    public void Add(int altitude, int ruleId)
    {
        if (!rulesIndex.TryGetValue(altitude, out var list))
        {
            list = new List<int>();
            rulesIndex[altitude] = list;
        }

        list.Add(ruleId);
    }

    public IEnumerable<int> Altitudes => rulesIndex.Keys;

    public List<int> Get(int altitude)
    {
        return rulesIndex[altitude];
    }
}