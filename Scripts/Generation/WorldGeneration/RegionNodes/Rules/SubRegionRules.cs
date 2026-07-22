using System;
using System.Collections.Generic;

namespace ProjectAlps.Generation.WorldGeneration.RegionNodes.Rules
{
    public class SubRegionRules{
        
    public int Id { get; set; }

    public string Name { get; set; }

    public GenerationRules GenerationRules { get; set; }

    public OccurrenceRules OccurrenceRules { get; set; }

    public LinkRules LinkRules { get; set; }

    public PropertiesRules PropertiesRules { get; set; }
    }
}