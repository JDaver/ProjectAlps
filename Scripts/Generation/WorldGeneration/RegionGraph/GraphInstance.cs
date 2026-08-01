using System;
using System.Collections.Generic;
using ProjectAlps.Generation.WorldGeneration.RegionNodes;

namespace ProjectAlps.Generation.WorldGeneration.RegionGraph;

public class GraphInstance
{
    public RegionGraph Graph { get; set; }

    public GraphInstance()
    {
        Graph = new RegionGraph();
    }

    public void GenerateGraph(RegionNodeInstance nodesInstance,ArchetypeInstance archetypeInstance)
    {
        RegionNode startNode = ExtractMinorAltitudeNode(nodesInstance);
        Graph.AddNode(startNode);   

        //TODO probably better using queue datastructure to perform BFS similar concept for generating graph

        // Generate edges based on the archetype's rules with empty nodes.

        //ChildrenGeneration score-based

        //if archetype allows, generates loops
        
    }

    private RegionNode ExtractMinorAltitudeNode(RegionNodeInstance nodesInstance)
    {
        int minElevation = nodesInstance.RegionsCollection.Keys.Min();
        return nodesInstance.RegionsCollection[minElevation];
    }
}