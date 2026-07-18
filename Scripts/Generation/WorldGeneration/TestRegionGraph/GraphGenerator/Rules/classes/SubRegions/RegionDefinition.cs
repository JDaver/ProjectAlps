using System.Collections.Generic;

public class RegionDefinition
{
    public int Id { get; set; }

    public string Name { get; set; }

    public GenerationRules GenerationRules { get; set; }

    public OccurrenceRules OccurrenceRules { get; set; }

    public LinkRules LinkRules { get; set; }

    public PropertiesRules PropertiesRules { get; set; }


}