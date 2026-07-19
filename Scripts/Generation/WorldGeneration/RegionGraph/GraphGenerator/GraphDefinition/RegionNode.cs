using System;
using System.Collections.Generic;

namespace ProjectAlps.Generation.WorldGeneration.RegionGraph
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


        public RegionNode(int id, string name)
        {
            Id = id;
            Name = name;
            
            Neighbours = new HashSet<RegionNode>();
        }
    }
}