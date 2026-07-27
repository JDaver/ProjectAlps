using System;
using ProjectAlps.Generation.WorldGeneration.WorldArchetype.Rules;
using ProjectAlps.Generation.WorldGeneration.WorldArchetype.Instance;
using ProjectAlps.Generation.WorldGeneration.WorldArchetype.Loader;
using ProjectAlps.Generation.WorldGeneration.SeedExporter;
using ProjectAlps.Generation.WorldGeneration.RegionNodes;

class Program
{
    static void Main()
    {
        int seed = SeedTestExporter.Seed; 
        ArchetypeLoader loader = new();

        NodeLoader nodeLoader = new();

        ArchetypeInstance Instance = new ArchetypeInstance(loader,seed );

        RegionNodesInstance regions = new RegionNodesInstance(seed,Instance, nodeLoader);

            Console.WriteLine("==============================");
            Console.WriteLine($"ARCHETYPE {Instance.Id}");
            Console.WriteLine("==============================");

            Console.WriteLine($"Name: {Instance.Name}");

            Console.WriteLine("\nRegion Rules:");
            Console.WriteLine(
                $"Number of Regions: {Instance.NumberRegions}"
            );

            Console.WriteLine("\nGraph Rules:");
            Console.WriteLine(
                $"Min Links: {Instance.Connettivity.MinLinksPerNode}"
            );
            Console.WriteLine(
                $"Max Links: {Instance.Connettivity.MaxLinksPerNode}"
            );
            Console.WriteLine(
                $"Branching Preference: {Instance.Connettivity.BranchingPreference}"
            );
            Console.WriteLine(
                $"Allow Loops: {Instance.Connettivity.AllowLoops}"
            );

            Console.WriteLine("\nElevation Rules:");
            Console.WriteLine(
                $"Type: {Instance.ElevationProfile.Type}"
            );
            Console.WriteLine(
                $"Weight: {Instance.ElevationProfile.Weight}"
            );

            Console.WriteLine();

              Console.WriteLine("Generated Regions:");

            foreach(var region in regions.RegionsCollection)
            {
                RegionNode node = region.Value;

                Console.WriteLine(
                    $"ID: {node.Id} | Name: {node.Name}"
                );
            }
    }
}


// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Text.Json;
// using ProjectAlps.Generation.WorldGeneration.WorldArchetype.Loader;
// using ProjectAlps.Generation.WorldGeneration.WorldArchetype.Instance;
// using ProjectAlps.Generation.WorldGeneration.RegionNodes;

// public class WorldTestDTO
// {
//     public int Seed { get; set; }
//     public int ArchetypeId { get; set; }
//     public string ArchetypeName { get; set; }
//     public List<RegionTestDTO> Regions { get; set; }
// }

// public class RegionTestDTO
// {
//     public int Id { get; set; }
//     public string Name { get; set; }
//     public float Elevation { get; set; }
// }


// class Program
// {
//     static void Main()
//     {
//         ArchetypeLoader loader = new();
//         NodeLoader nodeLoader = new();

//         Random rng = new Random(12345);

//         List<WorldTestDTO> worlds = new();

//         int instances = 100;

//         for(int i = 0; i < instances; i++)
//         {
//             int seed = rng.Next();

//             ArchetypeInstance instance =
//                 new ArchetypeInstance(loader, seed);

//             RegionNodesInstance regions =
//                 new RegionNodesInstance(
//                     seed,
//                     instance,
//                     nodeLoader
//                 );


//             WorldTestDTO world = new()
//             {
//                 Seed = seed,
//                 ArchetypeId = instance.Id,
//                 ArchetypeName = instance.Name,
//                 Regions = new List<RegionTestDTO>()
//             };


//             foreach(var region in regions.RegionsCollection.Values)
//             {
//                 world.Regions.Add(new RegionTestDTO
//                 {
//                     Id = region.Id,
//                     Name = region.Name,
//                     Elevation = region.Elevation
//                 });
//             }


//             worlds.Add(world);
//         }


//         string json = JsonSerializer.Serialize(
//             worlds,
//             new JsonSerializerOptions
//             {
//                 WriteIndented = true
//             }
//         );


//         File.WriteAllText(
//             "GeneratedWorldsTest.json",
//             json
//         );


//         Console.WriteLine("GeneratedWorldsTest.json created");
//     }
// }