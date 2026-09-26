using System;
using System.Collections.Generic;
using System.Numerics;
using RegionGraphType =
    ProjectAlps.Generation.WorldGeneration.RegionGraph.RegionGraph;
    using System;
using ProjectAlps.Generation.WorldGeneration.RegionNodes;

public class FruchtermanReingold
{
    private readonly Random rng;
    private float k;

    public FruchtermanReingold(int seed)
    {
        rng = new Random(seed);
    }

    private float RepulsionForce(float distance){
        return k * k / distance;
    }

    private float AttractionForce(float distance){
        return distance * distance / k;
    }

    public Dictionary<RegionNode, Vector2> GenerateLayout(
        RegionGraphType graph,
        RegionNode center,
        float width,
        float height,
        int iterations = 500
        )
    {
        int n = graph.Nodes.Count;

        // k = optimal distance between vertices
        float area = width * height;
        k = MathF.Sqrt(area / n);

        //center node is fixed in (0,0)
        Dictionary<RegionNode, Vector2> positions = new();

        foreach (RegionNode node in graph.Nodes.Values)
        {
            if (node == center)
            {
            positions[node] = new Vector2(width/2,height/2);
            continue;
            }else{
                positions[node] = new Vector2(
                (float)rng.NextDouble() * width,
                (float)rng.NextDouble() * height 
            );
            }
            
        }

        // entropy controller
        float temperature = width / 10f;
        float cooling = temperature / iterations;

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

                    Vector2 delta =
                        positions[v] - positions[u];

                    float distance = delta.Length();

                    if (distance < 0.01f)
                        distance = 0.01f;

                    Vector2 direction =
                        delta / distance;

                    float force = RepulsionForce(distance);

                    displacement[v] +=
                        direction * force;
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

                    Vector2 delta =
                        positions[v] - positions[u];

                    float distance = delta.Length();

                    if (distance < 0.01f)
                        distance = 0.01f;

                    Vector2 direction =
                        delta / distance;

                    float force = AttractionForce(distance);

                    displacement[v] -=
                        direction * force;
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
                    // limit movements
                    float limitedLength =
                        MathF.Min(length, temperature);

                    disp =
                        disp / length * limitedLength;
                }

                positions[node] += disp;

                // -------------------------------------------------
                // 4. BORDERS
                // -------------------------------------------------

            //    positions[node] = new Vector2(
            //         Math.Clamp(
            //             positions[node].X,
            //             -width / 2f,
            //             width / 2f),

            //         Math.Clamp(
            //             positions[node].Y,
            //             -height / 2f,
            //             height / 2f)
            //     );
            }

            // -------------------------------------------------
            // 5. COOLING
            // -------------------------------------------------

            temperature -= cooling;
        }

        return positions;
    }
}