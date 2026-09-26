using System;
using System.Numerics;
using System.Globalization;
using ProjectAlps.Generation.WorldGeneration.WorldArchetype.Rules;
using ProjectAlps.Generation.WorldGeneration.WorldArchetype.Instance;
using ProjectAlps.Generation.WorldGeneration.WorldArchetype.Loader;
using ProjectAlps.Generation.WorldGeneration.SeedExporter;
using ProjectAlps.Generation.WorldGeneration.RegionNodes;
using ProjectAlps.Generation.WorldGeneration.RegionGraph;
using ProjectAlps.Generation.WorldGeneration.WorldMatrix;

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

        Console.WriteLine("==============================");
        Console.WriteLine("EUCLIDEAN WORLD: ");
        Console.WriteLine("==============================");

        WorldMatrixInstance CurrentWorld = new WorldMatrixInstance(seed,graphInstance.Graph);
        char Letter = (char)65;

foreach (var pair in CurrentWorld.EuclideanWorld)
{
    RegionNode node = pair.Key;
    Vector2 position = pair.Value;

    Console.WriteLine(
        $"{Letter}=({position.X.ToString("F2", CultureInfo.InvariantCulture)}," +
        $"{position.Y.ToString("F2", CultureInfo.InvariantCulture)})"
    );

    Letter++;
}
    }
}