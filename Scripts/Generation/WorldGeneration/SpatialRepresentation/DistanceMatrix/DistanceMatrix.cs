using RegionGraphType =
    ProjectAlps.Generation.WorldGeneration.RegionGraph.RegionGraph;

using System;
using System.Collections.Generic;
using ProjectAlps.Generation.WorldGeneration.RegionNodes;
using ProjectAlps.Generation.WorldGeneration.SpatialRep.ScoreSystem;

public class DistanceMatrix
{
    public int[,] Matrix { get; private set; }

    public ScoreList DistanceScore { get; } = new ScoreList();

    private Dictionary<int, int> nodeToIndex = new();

    public DistanceMatrix(RegionGraphType graph)
    {
        Matrix = new int[graph.Nodes.Count, graph.Nodes.Count];

        int index = 0;

        foreach (RegionNode node in graph.Nodes.Values)
        {
            nodeToIndex[node.Id] = index;
            index++;
        }

        BreadthFirstSearch(graph);
        AssignScore(graph);

        DistanceScore.Print();
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

            int startIndex = nodeToIndex[startNode.Id];

            foreach (var pair in distances)
            {
                int neighbourIndex = nodeToIndex[pair.Key.Id];

                Matrix[startIndex, neighbourIndex] = pair.Value;
            }
        }
    }

    private void AssignScore(RegionGraphType graph)
    {
        int rows = Matrix.GetLength(0);
        int columns = Matrix.GetLength(1);

        foreach (RegionNode current in graph.Nodes.Values)
        {
            int index = nodeToIndex[current.Id];

            int partialSum = 0;

            for (int j = 0; j < columns; j++)
            {
                partialSum += Matrix[index, j];
            }

            DistanceScore.Insert(current, partialSum);
        }
    }
}