using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using ProjectAlps.Generation.WorldGeneration.RegionNodes.Rules;
using ProjectAlps.Utils.PathManager;

namespace ProjectAlps.Generation.WorldGeneration.RegionNodes;
public class NodeLoader{

    public Dictionary <int, SubRegionRules> RegionTypes { get; private set;}

    public NodeLoader(){
        RegionTypes = new Dictionary <int, SubRegionRules>();

        string json = File.ReadAllText(
            PathManager.RegionRules
        );

        SubRegionRulesDTO data =
            JsonSerializer.Deserialize<SubRegionRulesDTO>(json)
            ?? throw new Exception("Invalid RegionRules JSON");


        foreach(var node in data.SubRegions){
            int regionMeanAltitude = (int)node.GenerationRules.Elevation.Mean;
            int regionId = node.Id;

            RegionTypes.Add(regionMeanAltitude, node);
        }
    }
}