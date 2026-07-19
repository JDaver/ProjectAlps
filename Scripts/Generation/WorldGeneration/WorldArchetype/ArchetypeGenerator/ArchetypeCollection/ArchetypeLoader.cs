using System.IO;
using System.Text.Json;
using ProjectAlps.Generation.WorldGeneration.WorldArchetype.Rules;
namespace ProjectAlps.Generation.WorldGeneration.WorldArchetype.Loader;

public class ArchetypeLoader
{
    public WorldArchetypeRules[] Types { get; private set; }


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