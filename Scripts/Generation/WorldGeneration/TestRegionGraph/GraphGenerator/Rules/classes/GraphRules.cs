using System.Collections.Generic;

public class GraphRules
{
    public MainRegionRules MainRegion { get; set; } = new();

    public List<RegionDefinition> SubRegions { get; set; } = new();

    public void print(){
            Console.WriteLine("=== Main Region Rules ===");

        Console.WriteLine(
            $"Min Regions: {MainRegion.MinRegions}"
        );

        Console.WriteLine(
            $"Max Regions: {MainRegion.MaxRegions}"
        );

        Console.WriteLine(
            $"Starting Region: {MainRegion.StartingRegion}"
        );

        Console.WriteLine(
            $"Ending Region: {MainRegion.EndingRegion}"
        );


        Console.WriteLine("\n=== Sub Regions ===");


        foreach(var region in SubRegions)
        {
            Console.WriteLine(
                $"[{region.Id}] {region.Name}"
            );

                Console.WriteLine(
                $"   Occurrence min: {region.OccurrenceRules.Min}"
            );

                Console.WriteLine(
                $"   Occurrence max: {region.OccurrenceRules.Max}"
            );

            Console.WriteLine(
                $"   Min Links: {region.LinkRules.MinLinks}"
            );


            Console.WriteLine(
                $"   Max Links: {region.LinkRules.MaxLinks}"
            );


            Console.WriteLine(
                $"   Neighbours: {string.Join(", ", region.LinkRules.Neighbours)}"
            );


            Console.WriteLine();
            }
           
    }
}