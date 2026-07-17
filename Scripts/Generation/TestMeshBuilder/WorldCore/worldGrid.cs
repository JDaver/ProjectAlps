public class WorldGrid{
	public int SizeX;
	public int SizeZ;
	
	public float [,] Height;
	public int [,] Biome;
	
	public WorldGrid(int x, int z){
		SizeX = x;
		SizeZ = z;
		
		Height = new float [x,z];
		Biome = new int [x,z];
	}
}
