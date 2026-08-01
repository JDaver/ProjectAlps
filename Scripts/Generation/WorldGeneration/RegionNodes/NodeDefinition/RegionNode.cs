using System;
using System.Collections.Generic;


namespace ProjectAlps.Generation.WorldGeneration.RegionNodes
{
    public class RegionNode
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Extent { get; set; }

        public float Elevation { get; set; }

        public bool isStart { get; set; }
         
        public bool isEnd { get; set; }

        public HashSet<RegionNode> Neighbours { get; set; }


        public RegionNode(int id, string name, int elevation)
        {
            Id = id;
            Name = name;
            Neighbours = new HashSet<RegionNode>();
            Elevation = elevation;
        }

        public void AddNeighbour(RegionNode currentNeighbour)
        {
            Neighbours.Add(currentNeighbour);
            currentNeighbour.Neighbours.Add(this);
        }
    }
}