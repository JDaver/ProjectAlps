using System.IO;
using System.Text.Json;
using ProjectAlps.Generation.WorldGeneration.WorldArchetype.Rules;
using ProjectAlps.Utils.PathManager;

namespace ProjectAlps.Generation.WorldGeneration.WorldArchetype.Loader;

public class ArchetypeLoader
{
    public WorldArchetypeRules[] Types { get; private set; }


    public ArchetypeLoader()
    {
        string json = File.ReadAllText(
            PathManager.WorldArchetypeRules
        );

        WorldArchetypeRulesDTO data =
            JsonSerializer.Deserialize<WorldArchetypeRulesDTO>(json);


        Types = data.WorldArchetypes;
    }
}