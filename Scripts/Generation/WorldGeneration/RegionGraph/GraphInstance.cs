using System;
using System.Collections.Generic;
using System.Linq;
using ProjectAlps.Generation.WorldGeneration.RegionNodes;
using ProjectAlps.Generation.WorldGeneration.RegionNodes.Rules;
using  ProjectAlps.Generation.WorldGeneration.RegionGraph.utilsDS;
using ProjectAlps.Generation.WorldGeneration.WorldArchetype.Instance;

namespace ProjectAlps.Generation.WorldGeneration.RegionGraph;
public class GraphInstance
{
    public RegionGraph Graph { get; set; }

    private List<AvailableNode> availableNodes = new();
    private RegionNodesInstance NodesInstance {get; set;}
    private Random rng;
    private Queue<RegionNode> queue;
    private float maxElevationDistance {get; set;}

    public GraphInstance(RegionNodesInstance nodesInstance, int seed)
    {
        Graph = new RegionGraph();
        
        rng = new Random(seed);

        NodesInstance = nodesInstance;
        availableNodes = nodesInstance.RegionsCollection
        .Values
        .GroupBy(n => n.RegionTypeId)
        .Select(g => new AvailableNode(
            g.ToList()
        ))
        .ToList();

        queue = new Queue<RegionNode>();

        maxElevationDistance = CalculateMaxElevationDistance();

    }

    public void GenerateGraph(ArchetypeInstance archetypeInstance,NodeLoader nodeLoader)
    {
        int numberOfRegions = archetypeInstance.NumberRegions;
        float branchingPreference = archetypeInstance.Connettivity.BranchingPreference;
        float loopsProbability = archetypeInstance.Connettivity.LoopsProbability;
        int minLinks = archetypeInstance.Connettivity.MinLinksPerNode;
        int maxLinks = archetypeInstance.Connettivity.MaxLinksPerNode;

        RegionNode startNode = ExtractMinorAltitudeNode();
        Graph.AddNode(startNode);  
        queue.Enqueue(startNode);

        while(queue.Count > 0 && Graph.Nodes.Count < numberOfRegions)
        {

        //extract node from Queue
        RegionNode current = queue.Dequeue();
        // float currentDistanceTarget = current.Elevation;

        //Link Calculus
        int numberOfLinks =  CalculateNumberOfLinks(minLinks, maxLinks, branchingPreference);

        for( int j = 0; j < numberOfLinks; j++){
            //Pick hightest score node 
            if(availableNodes.Count == 0)
                break;
            if(Graph.Nodes.Count >= numberOfRegions)
                break;

            AvailableNode candidate = PickHighestScoreNode(current);

            if(candidate == null)
                break;

            
            RegionNode node = candidate.Consume();

            Graph.AddNode(node);
            Graph.AddEdge(current, node);

            ConsumeNode(candidate);

            queue.Enqueue(node);
        }
             
        }
        
        //TODO if archetype allows, generates loops
        if(archetypeInstance.Connettivity.AllowLoops)
        {
            Console.WriteLine("AllowLoops True!");
            GenerateLoops(startNode,nodeLoader, loopsProbability);
        }
            
    }


    private void GenerateLoops(
        RegionNode startNode,
        NodeLoader nodeLoader,
        float loopsProbability)
    {
        Queue<RegionNode> bfsQueue = new();
        HashSet<RegionNode> visited = new();

        bfsQueue.Enqueue(startNode);
        visited.Add(startNode);


        while(bfsQueue.Count > 0)
        {
            RegionNode current = bfsQueue.Dequeue();


            List<RegionNode> neighbours =
                current.Neighbours.ToList();


            // prova tutte le coppie di vicini
            for(int i = 0; i < neighbours.Count; i++)
            {
                for(int j = i + 1; j < neighbours.Count; j++)
                {
                    ValidateLoop(
                        neighbours[i],
                        neighbours[j],
                        nodeLoader,
                        loopsProbability);
                }
            }


            foreach(var neighbour in neighbours)
            {
                if(visited.Add(neighbour))
                    bfsQueue.Enqueue(neighbour);
            }
        }
    }
      //loopValidation
    private void ValidateLoop(
        RegionNode a,
        RegionNode b,
        NodeLoader nodeLoader,
        float loopsProbability)
    {
        if(a.Neighbours.Contains(b) || rng.NextSingle() > loopsProbability)
            return;


        SubRegionRules rulesA =
            nodeLoader.RegionTypes[a.RegionTypeId];

        SubRegionRules rulesB =
            nodeLoader.RegionTypes[b.RegionTypeId];


        bool compatible =
            rulesA.LinkRules.Neighbours.Contains(b.RegionTypeId)
            &&
            rulesB.LinkRules.Neighbours.Contains(a.RegionTypeId);


        if(compatible)
        {
            Graph.AddEdge(a,b);
        }
    }

   

    //Pick HightestScore
    private AvailableNode PickHighestScoreNode( RegionNode current)
    {
        return availableNodes
            .MaxBy(candidate =>
                CalculateScore(current, candidate));
    }

    private float CalculateScore(
    RegionNode current,
    AvailableNode candidate)
    {
        RegionNode node = candidate.Peek();

        float distance = Math.Abs(
            current.Elevation - node.Elevation
        );

        float distanceScore = 
            1f - Math.Clamp(distance / maxElevationDistance, 0f, 1f);

        float availability =
        1f / candidate.Occurrences;


        float cluster =
            CalculateClusterPenalty(current, candidate);


        return distanceScore * availability;
    }

    private float CalculateClusterPenalty(
    RegionNode current,
    AvailableNode candidate)
    {
       int sameTypeNeighbours = current.Neighbours
        .Count(n => n.RegionTypeId == candidate.RegionTypeId);

        return 1f / (1f + sameTypeNeighbours);
    }

  
    
    //general Utils
    private RegionNode ExtractMinorAltitudeNode()
    {
        AvailableNode candidate = availableNodes
            .OrderBy(n => n.Peek().Elevation)
            .First();

        RegionNode node = candidate.Consume();

        if(candidate.Occurrences == 0)
        {
            availableNodes.Remove(candidate);
        }

        return node;
    }

    private int CalculateNumberOfLinks(
    int minLinks,
    int maxLinks,
    float branchingPreference)
    {
        int links = minLinks;

        for (int i = minLinks; i < maxLinks; i++)
        {
            if (rng.NextSingle() < branchingPreference)
            {
                links++;
            }
        }

        return links;
    }

    private float CalculateMaxElevationDistance()
    {
        float min = availableNodes
            .Min(n => n.Peek().Elevation);

        float max = availableNodes
            .Max(n => n.Peek().Elevation);

        return Math.Max(max - min, 1f);
    }

    private void ConsumeNode(AvailableNode available)
    {
        if(available.Occurrences == 0)
        {
            availableNodes.Remove(available);
        }
    }
}