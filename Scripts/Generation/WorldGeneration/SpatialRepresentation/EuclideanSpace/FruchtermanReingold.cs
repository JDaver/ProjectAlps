using System;
using System.Collections.Generic;
using System.Numerics;
using RegionGraphType =
    ProjectAlps.Generation.WorldGeneration.RegionGraph.RegionGraph;

using ProjectAlps.Generation.WorldGeneration.RegionNodes;
using ProjectAlps.Generation.WorldGeneration.WorldArchetype.Rules;

public class FruchtermanReingold
{
    private readonly Random rng;

    private float k;
    private float width;
    private float height;

    private GeometricRules GeometricRules { get; set; }

    public FruchtermanReingold(int seed,GeometricRules currentRules )
    {
        rng = new Random(seed);
        GeometricRules = currentRules;
    
    }

    private float RepulsionForce(float distance)
    {
        return k * k / distance;
    }

    private float AttractionForce(float distance)
    {
        return distance * distance / k * 0.5f;
    }

    public Dictionary<RegionNode, Vector2> GenerateLayout(
        RegionGraphType graph,
        RegionNode center,
        int iterations = 500
    )
    {

        // -------------------------------------------------
        // MAP PARAMETERS
        // -------------------------------------------------
        int n = graph.Nodes.Count;
        float aspectRatio = GeometricRules.AspectRatio;
        float area = GeometricRules.Area;
        width = MathF.Sqrt(area / aspectRatio);
        height = width * aspectRatio;

        // -------------------------------------------------
        // FR PARAMETERS
        // -------------------------------------------------

        // Standard FR distance
         k = MathF.Sqrt(area / n);


        // Entropy controller
        float temperature = k ;
        float cooling = temperature / iterations;

        // -------------------------------------------------
        // INITIAL POSITIONS
        // -------------------------------------------------

        Dictionary<RegionNode, Vector2> positions = new();

        foreach (RegionNode node in graph.Nodes.Values)
        {
            if (node == center)
            {
                positions[node] = new Vector2(0,0);
                continue;
            }

            positions[node] = new Vector2(
                (float)rng.NextDouble() * width,
                (float)rng.NextDouble() * height
            );
        }

        // -------------------------------------------------
        // FR ITERATIONS
        // -------------------------------------------------

        for (int iteration = 0; iteration < iterations; iteration++)
        {
            Dictionary<RegionNode, Vector2> displacement = new();

            foreach (RegionNode node in graph.Nodes.Values)
                displacement[node] = Vector2.Zero;

            // -------------------------------------------------
            // 1. REPULSION
            // -------------------------------------------------

            foreach (RegionNode v in graph.Nodes.Values)
            {
                foreach (RegionNode u in graph.Nodes.Values)
                {
                    if (v == u)
                        continue;

                    Vector2 delta = positions[v] - positions[u];

                    float distance = delta.Length();

                    if (distance < 0.01f)
                        distance = 0.01f;

                    Vector2 direction = delta / distance;

                    float force = RepulsionForce(distance);

                    displacement[v] += direction * force;
                }
            }

            // -------------------------------------------------
            // 2. ATTRACTION
            // -------------------------------------------------

            foreach (RegionNode v in graph.Nodes.Values)
            {
                foreach (RegionNode u in v.Neighbours)
                {
                    if (v == center)
                        continue;

                    Vector2 delta = positions[v] - positions[u];

                    float distance = delta.Length();

                    if (distance < 0.01f)
                        distance = 0.01f;

                    Vector2 direction = delta / distance;

                    float force = AttractionForce(distance);

                    displacement[v] -= direction * force;
                }
            }

            // -------------------------------------------------
            // 3. TRANSLATION
            // -------------------------------------------------

            foreach (RegionNode node in graph.Nodes.Values)
            {
                if (node == center) 
                    continue;

                Vector2 disp = displacement[node];

                float length = disp.Length();

                if (length > 0.01f)
                {
                    float limitedLength =
                        MathF.Min(length, temperature);

                    disp = disp / length * limitedLength;
                }

                positions[node] += disp;

                // -------------------------------------------------
                // BORDERS
                // -------------------------------------------------

                positions[node] = new Vector2(
                    Math.Clamp(positions[node].X, -width, width),
                    Math.Clamp(positions[node].Y, -height, height)
                );
            }

            // -------------------------------------------------
            // 4. COOLING
            // -------------------------------------------------

            temperature -= cooling;
        }

        return positions;
    }
}