using System;
using System.Collections.Generic;
using ProjectAlps.Generation.WorldGeneration.RegionNodes;

namespace ProjectAlps.Generation.WorldGeneration.RegionGraph;
public class RegionGraph
{
    public Dictionary<int, RegionNode> Nodes { get; }

    public RegionGraph()
    {
        Nodes = new Dictionary<int, RegionNode>();
    }

    public void AddNode(RegionNode node)
    {
        Nodes.Add(node.Id, node);
    }

    public void AddEdge(RegionNode a, RegionNode b)
    {
        a.AddNeighbour(b);
    }
}