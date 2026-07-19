using System;
using ProjectAlps.Generation.WorldGeneration.WorldArchetype;

class Program
{
    static void Main()
    {
        ArchetypeLoader loader = new();

        int index = 1;

        foreach (WorldArchetype archetype in loader.Types)
        {
            Console.WriteLine("==============================");
            Console.WriteLine($"ARCHETYPE {index}");
            Console.WriteLine("==============================");

            Console.WriteLine($"Id: {archetype.Id}");
            Console.WriteLine($"Name: {archetype.Name}");

            Console.WriteLine("\nRegion Rules:");
            Console.WriteLine(
                $"Min Regions: {archetype.RegionRules.MinRegions}"
            );
            Console.WriteLine(
                $"Max Regions: {archetype.RegionRules.MaxRegions}"
            );

            Console.WriteLine("\nGraph Rules:");
            Console.WriteLine(
                $"Min Links: {archetype.ConnettivityRules.MinLinksPerNode}"
            );
            Console.WriteLine(
                $"Max Links: {archetype.ConnettivityRules.MaxLinksPerNode}"
            );
            Console.WriteLine(
                $"Branching Preference: {archetype.ConnettivityRules.BranchingPreferences}"
            );
            Console.WriteLine(
                $"Allow Loops: {archetype.ConnettivityRules.AllowLoops}"
            );

            Console.WriteLine("\nElevation Rules:");
            Console.WriteLine(
                $"Type: {archetype.ElevationProfile.Type}"
            );
            Console.WriteLine(
                $"Weight: {archetype.ElevationProfile.Weight}"
            );

            Console.WriteLine();

            index++;
        }

        Console.WriteLine(
            $"Total archetypes loaded: {loader.Types.Length}"
        );
    }
}
