using System;
using System.Collections.Generic;


public class RegionGraph
{
    public Dictionary<int, RegionNode> Nodes { get; set; }
    public int Count { get;set;}

    public RegionGraph()
    {
        Nodes = new Dictionary<int, RegionNode>();
        Count = 0;
    }


    public void AddNode(RegionNode node)
    {
        Nodes.Add(node.Id, node);
        Count++;
    }


    public void AddConnection(
        RegionNode a,
        RegionNode b)
    {
        a.Neighbours.Add(b);
        b.Neighbours.Add(a);
    }


    public void Print()
    {
        foreach(var node in Nodes.Values)
        {
            Console.WriteLine(
                $"{node.Id} - {node.Name}"
            );


            foreach(var neighbour in node.Neighbours)
            {
                Console.WriteLine(
                    $"   -> {neighbour.Name}"
                );
            }
        }
    }
}