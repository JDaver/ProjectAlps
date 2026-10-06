
using System;
using System.Collections.Generic;
using System.Numerics;
using ProjectAlps.Generation.WorldGeneration.RegionNodes;

public class WorldMatrixDiscretization
{
    public int[,] WorldMatrix {get; private set;}
    public Dictionary<int,Vector2> keyValuePairs {get; private set;} = new();
    public int width;
    public int height;

    private static int PADDING = 5;
    private static int RESOLUTION = 2;

    float north = 0f, south = 0f, west = 0f, east = 0f;
    public WorldMatrixDiscretization(Dictionary<RegionNode, Vector2> spaceInstance)
    {
        CalculateSize(spaceInstance);
        WorldMatrix = new int[width,height];
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                WorldMatrix[x, y] = -1;
            }
        }
        DiscretizePositions(spaceInstance);
    }    

        private void CalculateSize(
        Dictionary<RegionNode, Vector2> spaceInstance)
    {
        north = float.MinValue;
        south = float.MaxValue;
        west = float.MaxValue;
        east = float.MinValue;
    
        foreach (var point in spaceInstance.Values)
        {
            north = Math.Max(north, point.Y);
            south = Math.Min(south, point.Y);
            west = Math.Min(west, point.X);
            east = Math.Max(east, point.X);
        }
    
        int contentWidth = (int)Math.Ceiling((east - west)* RESOLUTION) + 1;
        int contentHeight = (int)Math.Ceiling((north - south)* RESOLUTION) + 1;
    
        width = contentWidth + PADDING * 2;
        height = contentHeight + PADDING * 2;
    }

      private void DiscretizePositions(
        Dictionary<RegionNode, Vector2> spaceInstance)
    {
        foreach (var node in spaceInstance)
        {
            Vector2 point = node.Value;

            int x = (int)Math.Round((point.X - west)* RESOLUTION) + PADDING;
            int y = (int)Math.Round((point.Y - south)* RESOLUTION )+ PADDING;

            Vector2 newPointCoordinates = new Vector2(x,y);

            WorldMatrix[x, y] = node.Key.Id;

            keyValuePairs.Add(node.Key.Id, newPointCoordinates);
        }
    }

    public void PrintMatrix()
{
    for (int y = 0; y < height; y++)
    {
        for (int x = 0; x < width; x++)
        {
            Console.Write($"{WorldMatrix[x, y],2}");
        }

        Console.WriteLine();
    }
}
}