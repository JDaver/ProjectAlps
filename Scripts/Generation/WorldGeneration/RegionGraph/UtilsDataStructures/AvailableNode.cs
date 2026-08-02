using System.Collections.Generic;
using ProjectAlps.Generation.WorldGeneration.RegionNodes;

namespace ProjectAlps.Generation.WorldGeneration.RegionGraph.utilsDS;

public class AvailableNode
{
    public int RegionTypeId { get; private set; }

    private List<RegionNode> nodes;

    public int Occurrences => nodes.Count;


    public AvailableNode(List<RegionNode> nodes)
    {
        this.nodes = nodes;
        RegionTypeId = nodes[0].RegionTypeId;
    }


    public RegionNode Consume()
    {
        RegionNode node = nodes[0];
        nodes.RemoveAt(0);

        return node;
    }

    public RegionNode Peek()
    {
        return nodes[0];
    }
}