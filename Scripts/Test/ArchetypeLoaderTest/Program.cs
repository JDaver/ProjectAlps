using System;
using ProjectAlps.Generation.WorldGeneration.WorldArchetype.Rules;
using ProjectAlps.Generation.WorldGeneration.WorldArchetype.Instance;
using ProjectAlps.Generation.WorldGeneration.WorldArchetype.Loader;

class Program
{
    static void Main()
    {
        ArchetypeLoader loader = new();

        ArchetypeInstance Instance = new ArchetypeInstance(loader, 231);

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
    }
}
