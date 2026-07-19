using System.Collections.Generic;


public class RegionGraphGenerator
{
    private int InternalNodesCounter = 0;
    private int numberOfRegions = 0;
    private List<RegionNode> listNode = new List<RegionNode>();

    public RegionGraph Generate(GraphRules rules, int ModuleSeed)
    {

        RegionGraph graph = new();
        int maxNumberOfRegions = ExtractNumber(rules.MinRegions, rules.MaxRegions, ModuleSeed);

        foreach (var region in rules.SubRegions)
        {
            listNode.Add(new RegionNode(region.Id, region.Name));
            numberOfRegions++;  
        }

        graph.AddNode(listNode[0]);
        graph.GenerateNodes(rules, ModuleSeed);

        return graph;
    }

    // V1 algo for generating map graphs
    private void GenerateNodes(GraphRules rules, int seed){
        while (InternalNodesCounter < MaxnumberOfRegions){
            RegionNode currentNode = listNode[InternalNodesCounter];
           //TODO
        }


    }


   //probably to change to a more generic function that can extract numbers from a range of values
    private int ExtractNumber(int min,int max, int seed){
        int length = max - min + 1;
        int number = (seed % length) + min;
        return number;
    }

    private T PickRandom<T>(T[] array, int seed){

        int index = Math.Abs(seed % array.Length);

        return array[index];
    }
}