using System.IO;
using System.Text.Json;
using ProjectAlps.Generation.WorldGeneration.RegionNodes.Rules;
using ProjectAlps.Utils.PathManager;

namespace ProjectAlps.Generation.WorldGeneration.RegionNodes.NodeLoader;
public class NodeLoader{

    public SubRegionRules[] RegionTypes { get; private set;}

    public NodeLoader(){
        string json = File.ReadAllText(
            PathManager.RegionRules
        );

        SubRegionRulesDTO data = 
            JsonSerializer.Deserialize<SubRegionRulesDTO>(json);


        RegionTypes = data.SubRegionRules;
    }
}