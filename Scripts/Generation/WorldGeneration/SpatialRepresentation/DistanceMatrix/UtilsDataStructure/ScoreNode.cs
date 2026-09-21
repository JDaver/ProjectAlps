using ProjectAlps.Generation.WorldGeneration.RegionNodes;

namespace ProjectAlps.Generation.WorldGeneration.SpatialRep.ScoreSystem;
public class ScoreNode
{
    public RegionNode Region { get; }
    public int Score { get; }

    public ScoreNode? Previous { get; set; }
    public ScoreNode? Next { get; set; }

    public ScoreNode(RegionNode region, int score){
        Region = region;
        Score = score;
    }
}