using System.IO;
using System.Text.Json;
using ProjectAlps.Generation.WorldGeneration.WorldArchetype;

public class ArchetypeLoader
{
    public WorldArchetype[] Types { get; private set; }


    public ArchetypeLoader()
    {
        string json = File.ReadAllText(
            "../../Generation/WorldGeneration/WorldArchetype/Rules/WorldArchetypeRules.json"
        );

        WorldArchetypeRulesDTO data =
            JsonSerializer.Deserialize<WorldArchetypeRulesDTO>(json);


        Types = data.WorldArchetypes;
    }
}