using System;
using System.Collections.Generic;
using System.Linq;
using RegionGraphType =
    ProjectAlps.Generation.WorldGeneration.RegionGraph.RegionGraph;
public class RegionFilling
{
    private const int EMPTY_CELL = -1;
    private const double RNG_PAR = 10.0;
    private const int SMOOTHING_STEPS = 3;
    
    public static void FillingAlgo(int seed, WorldMatrixDiscretization instance,RegionGraphType graph)
    {
        // DECLARATIONS

        int currentStep;
        int nextStep;
        double weight;
        int idx;
        int currentX;
        int currentY;
        int nextX;
        int nextY;
        int[,] smoothedMap;
        List<int> neighbours;
        int mode;

        PriorityQueue<(int step, int idx, int X, int Y), double> cellQueue = new();
        
        Random rng = new(seed);

        // INITIALIZATION

        foreach(var regionId in instance.keyValuePairs)
        {
            weight = (rng.NextDouble() * 2.0 - 1.0) * RNG_PAR;
            cellQueue.Enqueue((0, regionId.Key, (int)regionId.Value.X, (int)regionId.Value.Y), priority: weight);
        }

        // REGION FILLING

        while (cellQueue.Count > 0)
        {
            (currentStep, idx, currentX, currentY) = cellQueue.Dequeue();

            instance.WorldMatrix[currentX, currentY] = idx;

            for(int i =-1; i <=1; i++)
            {
                for(int j =-1; j<= 1; j++)
                {
                    if (i == 0 && j == 0) continue;

                    nextX = currentX + i;
                    nextY = currentY + j;

                    if (nextX < 0 || nextX >= instance.width ||
                        nextY < 0 || nextY >= instance.height)
                        continue;
                    
                    if (instance.WorldMatrix[nextX, nextY] == EMPTY_CELL)
                    {
                        nextStep = currentStep + 1;
                        weight = nextStep + (rng.NextDouble() * 2.0 - 1.0) * RNG_PAR;
                        cellQueue.Enqueue((nextStep, idx, nextX, nextY), weight);
                    }
                }
            }
        }

        // SMOOTHING

        for(currentStep = 0; currentStep < SMOOTHING_STEPS; currentStep++)
        {
            smoothedMap = (int[,])instance.WorldMatrix.Clone();

            for(currentX = 0; currentX <instance.width; currentX++)
            {
                for(currentY = 0; currentY <instance.height; currentY++)
                {
                    neighbours = new List<int>();

                    for(int i =-1; i <=1; i++)
                    {
                        for(int j =-1; j <=1; j++)
                        {
                            if (i == 0 && j == 0) continue;

                            nextX = currentX + i;
                            nextY = currentY + j;

                            if (nextX < 0 || nextX >= instance.width ||
                                nextY < 0 || nextY >= instance.height)
                                continue;

                            if (instance.WorldMatrix[nextX, nextY] == EMPTY_CELL) continue;

                            neighbours.Add(instance.WorldMatrix[nextX, nextY]);
                        }
                    }
                    mode = neighbours.GroupBy(v => v)
                                    .OrderByDescending(g => g.Count())
                                    .First().Key;
                    
                    smoothedMap[currentX, currentY] = mode;
                }
            }

            for(currentX =0; currentX <instance.width; currentX++)
            {
                for(currentY =0; currentY <instance.height; currentY++)
                {
                    instance.WorldMatrix[currentX,currentY] = smoothedMap[currentX,currentY];
                }
            }
        }
    }
}