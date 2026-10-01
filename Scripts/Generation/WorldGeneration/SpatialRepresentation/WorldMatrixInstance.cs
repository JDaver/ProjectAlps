using RegionGraphType =
    ProjectAlps.Generation.WorldGeneration.RegionGraph.RegionGraph;
using ProjectAlps.Generation.WorldGeneration.WorldArchetype.Instance;
using ProjectAlps.Generation.WorldGeneration.RegionNodes;
using System.Collections.Generic;
using System.Numerics;

namespace ProjectAlps.Generation.WorldGeneration.WorldMatrix;
public class WorldMatrixInstance
{
    public Dictionary<RegionNode,Vector2> EuclideanWorld;
    public FruchtermanReingold LayoutModel;

    public WorldMatrixInstance(int seed, RegionGraphType graph,ArchetypeInstance instance){
        LayoutModel = new FruchtermanReingold(seed,instance.GeometricRules);

        DistanceMatrix CurrentGraph = new DistanceMatrix(graph);
        RegionNode centerNode = CurrentGraph.DistanceScore._head.Region;

        EuclideanWorld = LayoutModel.GenerateLayout(graph, centerNode);
    }
}