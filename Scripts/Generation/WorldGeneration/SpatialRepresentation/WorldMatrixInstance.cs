using RegionGraphType =
    ProjectAlps.Generation.WorldGeneration.RegionGraph.RegionGraph;
    using System;
using ProjectAlps.Generation.WorldGeneration.RegionNodes;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace ProjectAlps.Generation.WorldGeneration.WorldMatrix;
public class WorldMatrixInstance
{
    public Dictionary<RegionNode,Vector2> EuclideanWorld;
    public FruchtermanReingold LayoutModel;

    public WorldMatrixInstance(int seed, RegionGraphType graph){
        LayoutModel = new FruchtermanReingold(seed);

        DistanceMatrix CurrentGraph = new DistanceMatrix(graph);
        RegionNode centerNode = CurrentGraph.DistanceScore._head.Region;

        EuclideanWorld = LayoutModel.GenerateLayout(graph, centerNode, 200, 200);
    }
}