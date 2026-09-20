using System;
using ProjectAlps.Generation.WorldGeneration.WorldArchetype.Rules;
using ProjectAlps.Generation.WorldGeneration.WorldArchetype.Instance;
using ProjectAlps.Generation.WorldGeneration.WorldArchetype.Loader;
using ProjectAlps.Generation.WorldGeneration.SeedExporter;
using ProjectAlps.Generation.WorldGeneration.RegionNodes;
using ProjectAlps.Generation.WorldGeneration.RegionGraph;
using ProjectAlps.Generation.WorldGeneration.DistanceMatrix;
using ProjectAlps.Generation.WorldGeneration.DistanceMatrix;

class Program
{
    static void Main()
    {
        int seed = SeedTestExporter.Seed;

        ArchetypeLoader loader = new();
        NodeLoader nodeLoader = new();

        ArchetypeInstance instance = new ArchetypeInstance(loader, seed);
        Console.WriteLine("==============================");
        Console.WriteLine($"ARCHETYPE: {instance.Name}");
        Console.WriteLine("==============================");
        RegionNodesInstance regions = 
            new RegionNodesInstance(seed, instance, nodeLoader);


        GraphInstance graphInstance = new GraphInstance(
            regions,
            seed
        );


        graphInstance.GenerateGraph(instance,nodeLoader);


        Console.WriteLine("==============================");
        Console.WriteLine("GENERATED GRAPH");
        Console.WriteLine("==============================");

        DistanceMatrix matrix = new DistanceMatrix(graphInstance.Graph);

        foreach(var nodeEntry in graphInstance.Graph.Nodes)
        {
            RegionNode node = nodeEntry.Value;


            Console.WriteLine(
                $"NODE {node.Id} | {node.Name} | Elevation: {node.Elevation}"
            );


            Console.WriteLine("  Links:");

            foreach(RegionNode neighbour in node.Neighbours)
            {
                Console.WriteLine(
                    $"    -> {neighbour.Id} | {neighbour.Name} | {neighbour.Elevation}"
                );
            }


            Console.WriteLine();
        }


        Console.WriteLine("==============================");
        Console.WriteLine(
            $"Total Nodes: {graphInstance.Graph.Nodes.Count}"
        );
        Console.WriteLine("==============================");
    }
}