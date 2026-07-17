using System;
using System.IO;
using System.Text.Json;

class Program
{
    static void Main(string[] args)
    {
        //JSON file
        string json = File.ReadAllText("GraphGenerator/Rules/GraphRules.json");
        GraphRules rules = JsonSerializer.Deserialize<GraphRules>(json);
        WorldSeed GraphSeed = new(1234);
        private int seedForGraph = GraphSeed.GetModuleSeed('graph');

        RegionGraphGenerator generator = new();

        RegionGraph graph = generator.Generate(rules, seedForGraph);

        Console.WriteLine("Graph generation test completed.");
    }
}