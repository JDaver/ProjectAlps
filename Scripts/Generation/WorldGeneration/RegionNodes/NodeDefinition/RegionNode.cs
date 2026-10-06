using System;
using System.Collections.Generic;


namespace ProjectAlps.Generation.WorldGeneration.RegionNodes
{
    public class RegionNode
    {
        public int Id { get; set; }

        public int RegionTypeId {get; set;}

        public string Name { get; set; }

        public int ExtentFactor { get; set; }

        public float Elevation { get; set; }

        public bool isStart { get; set; }
         
        public bool isEnd { get; set; }

        public HashSet<RegionNode> Neighbours { get; set; }


        public RegionNode(int id, int regionTypeId, string name, int elevation, int extentFactor)
        {
            Id = id;
            RegionTypeId = regionTypeId;
            Name = name;
            Neighbours = new HashSet<RegionNode>();
            Elevation = elevation;
            ExtentFactor = extentFactor;
        }

        public void AddNeighbour(RegionNode currentNeighbour)
        {
            Neighbours.Add(currentNeighbour);
            currentNeighbour.Neighbours.Add(this);
        }
    }
}