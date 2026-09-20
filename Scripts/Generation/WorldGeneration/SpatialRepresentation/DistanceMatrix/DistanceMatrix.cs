using RegionGraphType =
    ProjectAlps.Generation.WorldGeneration.RegionGraph.RegionGraph;
    using System;
using System.Collections.Generic;
using ProjectAlps.Generation.WorldGeneration.RegionNodes;

namespace ProjectAlps.Generation.WorldGeneration.DistanceMatrix;

public class DistanceMatrix{

    public int[,] Matrix { get; private set; }

    public DistanceMatrix(RegionGraphType graph)
    {
        Matrix = new int[graph.Nodes.Count, graph.Nodes.Count];

        BreadthFirstSearch(graph);
        PrintMatrix();
    }

private void BreadthFirstSearch(RegionGraphType graph)
{
    foreach (RegionNode startNode in graph.Nodes.Values)
    {
        Queue<RegionNode> queue = new();
        Dictionary<RegionNode, int> distances = new();

        queue.Enqueue(startNode);
        distances[startNode] = 0;

        while (queue.Count > 0)
        {
            RegionNode current = queue.Dequeue();

            foreach (RegionNode neighbour in current.Neighbours)
            {
                if (distances.ContainsKey(neighbour))
                    continue;

                distances[neighbour] =
                    distances[current] + 1;

                queue.Enqueue(neighbour);
            }
        }

        foreach (var pair in distances)
        {
            Matrix[startNode.Id, pair.Key.Id] = pair.Value;
        }
    }
}

    private void PrintMatrix()
{
    int rows = Matrix.GetLength(0);
    int columns = Matrix.GetLength(1);

    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < columns; j++)
        {
            Console.Write($"{Matrix[i, j],4}");
        }

        Console.WriteLine();
    }
}
}