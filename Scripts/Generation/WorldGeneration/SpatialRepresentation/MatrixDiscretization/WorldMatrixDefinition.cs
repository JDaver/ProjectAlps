
using System;
using System.Collections.Generic;
using System.Numerics;
using ProjectAlps.Generation.WorldGeneration.RegionNodes;

public class WorldMatrixDefinition
{
    public int[,] WorldMatrix {get; private set;}
    private int width;
    private int height;

    static int PADDING = 5;
    float north = 0f, south = 0f, west = 0f, east = 0f;
    public WorldMatrixDefinition(Dictionary<RegionNode, Vector2> spaceInstance)
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
        PrintMatrix();
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
    
        int contentWidth = (int)Math.Ceiling(east - west) + 1;
        int contentHeight = (int)Math.Ceiling(north - south) + 1;
    
        width = contentWidth + PADDING * 2;
        height = contentHeight + PADDING * 2;
    }

      private void DiscretizePositions(
        Dictionary<RegionNode, Vector2> spaceInstance)
    {
        foreach (var node in spaceInstance)
        {
            Vector2 point = node.Value;

            int x = (int)Math.Round(point.X - west) + PADDING;
            int y = (int)Math.Round(point.Y - south) + PADDING;

            WorldMatrix[x, y] = node.Key.Id;
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