using ProjectAlps.Generation.WorldGeneration.RegionNodes;
using System;
namespace ProjectAlps.Generation.WorldGeneration.SpatialRep.ScoreSystem;

public class ScoreList{
    private ScoreNode? _head;
    private ScoreNode? _tail;

    public void Insert(RegionNode region, int score){
        ScoreNode current = new ScoreNode(region,score);

        if(_head == null){
            _head = current;
            _tail = current;
            return;        
        }

        if(_head.Score > score){
            current.Next = _head;
            _head.Previous = current;
            _head = current;
            return;
        }

        ScoreNode? node = _head;

        while(node.Next != null && node.Next.Score <= score ){
            node = node.Next;
        }

        if(node.Next == null){
            current.Previous = node;
            node.Next = current;
            _tail = current;
            return;
        }

        current.Next = node.Next;
        current.Previous = node;
        node.Next.Previous = current;
        node.Next = current;

    }

    public void Print()
    {
        ScoreNode? node = _head;

        Console.WriteLine("========== SCORE LIST ==========");

        while (node != null)
        {
            Console.WriteLine(
                $"Score: {node.Score} | Region: {node.Region.Id}"
            );

            node = node.Next;
        }

        Console.WriteLine("================================");
    }

    public RegionNode? FindCompatible(RegionNode current)
    {
        ScoreNode? node = _head;

        while (node != null)
        {
            if (IsCompatible(current, node.Region))
            {
                RegionNode result = node.Region;

                Remove(node);

                return result;
            }

            node = node.Next;
        }

        return null;
    }

    private void Remove(ScoreNode node)
    {
        if (node.Previous != null)
            node.Previous.Next = node.Next;
        else
            _head = node.Next;

        if (node.Next != null)
            node.Next.Previous = node.Previous;
        else
            _tail = node.Previous;
    }

    private bool IsCompatible(RegionNode current, RegionNode candidate)
    {
        foreach(var neighbour in current.Neighbours){
            if(neighbour.RegionTypeId == candidate.RegionTypeId){
                return true;
            }
        }

        return false;
    }
}