using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using RegionGraphType =
    ProjectAlps.Generation.WorldGeneration.RegionGraph.RegionGraph;
public class RegionFilling
{
    private const int EMPTY_CELL = -1;
    public static void FillingAlgo(WorldMatrixDiscretization instance,RegionGraphType graph)
    {
        Dictionary<int,Queue<Vector2>> visited = new();

        //INITIALIZATION
        foreach(var regionId in instance.keyValuePairs)
        {
            Queue<Vector2> queue = new Queue<Vector2>();
            queue.Enqueue(regionId.Value);
            visited.Add(regionId.Key, queue);
        }

        while(visited.Any(x => x.Value.Count > 0)){
            foreach(var node in visited)
            {
                if (node.Value.Count == 0) continue;

                int extentFactor =1;
                int extentCounter = 0;
                int currentId = graph.Nodes[node.Key].Id;

                Vector2 currentCordinates = node.Value.Dequeue();

                int currentX = (int)currentCordinates.X;
                int currentY = (int)currentCordinates.Y;

    
                //check for every  direction
                while(extentCounter < extentFactor)
                {
                    for(int i =-1; i <= 1; i++)
                    {
                        for(int j =-1; j<=1; j++)
                        {
                            if (i == 0 && j == 0) continue;

                            int nextX = currentX + i;
                            int nextY = currentY + j;

                            if (nextX < 0 || nextX >= instance.width ||
                                nextY < 0 || nextY >= instance.height)
                            {
                                continue;
                            }

                            if (instance.WorldMatrix[nextX, nextY] == EMPTY_CELL)
                            {
                                instance.WorldMatrix[nextX, nextY] = currentId;

                                node.Value.Enqueue(
                                    new Vector2(nextX, nextY)
                                );
                            }
                        }
                    }
                extentCounter++;
                }
                
            }
        }

        
    }
}