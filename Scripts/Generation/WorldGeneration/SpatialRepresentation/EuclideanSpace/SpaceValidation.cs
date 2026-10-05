using System;
using System.Collections.Generic;
using ProjectAlps.Generation.WorldGeneration.RegionNodes;
using System.Numerics;
using System.Linq;

public class SpaceValidation
{
private List<(float Rad, Queue<RegionNode> Branch,RegionNode[] branchCollection)> Branches = new();
private List<(float min,float max)> Thetas = new();
private string Archetype;
private Random RNG;

static int NODES_EXCEED = 211;
static int BRANCHES_EXCEED = 4;
public SpaceValidation(int seed, string archetype)
    {
        RNG = new Random(seed);
        Archetype = archetype;
    }
public void ValidatePositions(Dictionary<RegionNode, Vector2> Positions)
    {
        if (Archetype == "VerticalClimb")
        {
            if(Positions.Count > NODES_EXCEED) return;
            RegionNode centerNode = Positions.First(x => x.Value == Vector2.Zero).Key;
            // --------------------------------------------------
            // BFS to find branches and their angles
            // --------------------------------------------------
            foreach (var neighbour in centerNode.Neighbours)
            {
                Queue<RegionNode> branch = new Queue<RegionNode>();

                float theta = MathF.Atan2(
                    Positions[neighbour].Y,
                    Positions[neighbour].X
                );

                branch.Enqueue(neighbour);
                Branches.Add((theta, branch, new RegionNode[] { neighbour }));
            }

            foreach (var branchData in Branches)
            {
                Queue<RegionNode> branch = branchData.Branch;

                HashSet<RegionNode> visited = new();

                float minRad = branchData.Rad;

                float maxRad = branchData.Rad;

                while (branch.Count > 0)
                {
                    RegionNode currentNode = branch.Dequeue();

                    if (!visited.Add(currentNode)) continue;

                    float currentTheta = MathF.Atan2(
                        Positions[currentNode].Y,
                        Positions[currentNode].X
                    );

                    if (currentTheta < minRad)
                        minRad = currentTheta;

                    if (currentTheta > maxRad)
                        maxRad = currentTheta;

                    foreach (RegionNode neighbour in currentNode.Neighbours)
                    {
                        if (neighbour == centerNode) continue;
                        branch.Enqueue(neighbour);
                    }
                }
                Thetas.Add((minRad, maxRad));
            }

            if(Thetas.Count >= BRANCHES_EXCEED) return;

            float deltaTheta = 0f;

            for (int i = 0; i < Thetas.Count; i++)
            {
                for (int j = i + 1; j < Thetas.Count; j++)
                {
                    float availableAngle =
                    Math.Min(
                        MathF.Abs(Thetas[i].min - Thetas[j].min),
                        MathF.Abs(Thetas[i].max - Thetas[j].max)
                    );
                    
                    double random = 0.2f + (RNG.NextDouble() * 0.8f);
                    
                    float newAngle = (float)(random * availableAngle);

                    Console.WriteLine(
                        $"Available angle between branch {i} and {j}: " +
                        $"{availableAngle}, New angle: {newAngle}"
                    );


                    deltaTheta =  newAngle - availableAngle;                    
                }

                CalculateRotations(Positions, deltaTheta, i, centerNode);

                Thetas[i] = (
        Thetas[i].min + deltaTheta,
        Thetas[i].max + deltaTheta
        );
        return;
        }
    }
    Console.WriteLine($"Validation for archetype {Archetype} is not required");
    return;
    }

    private void CalculateRotations(Dictionary<RegionNode, Vector2> Positions, float deltaTheta, int i, RegionNode centerNode) 
    {
        float cos = MathF.Cos(deltaTheta);
        float sin = MathF.Sin(deltaTheta);
        Queue<RegionNode> BFS = new Queue<RegionNode>();
        HashSet<RegionNode> visited = new() { centerNode };
        RegionNode node = Branches[i].branchCollection[0];
        BFS.Enqueue(node);
        while (BFS.Count > 0)
        {
            RegionNode currentNode = BFS.Dequeue();
            if (!visited.Add(currentNode))
                continue;
            Vector2 position = Positions[currentNode];
            Positions[currentNode] = new Vector2(
            position.X * cos - position.Y * sin,
            position.X * sin + position.Y * cos);

            Console.WriteLine(
                $"Current Node: {currentNode.Name}, " +
                $"Position: {Positions[currentNode]}");
            foreach (RegionNode neighbour in currentNode.Neighbours)
            {
                if (visited.Contains(neighbour))
                    continue;
                BFS.Enqueue(neighbour);
            }
        }    
    }

}