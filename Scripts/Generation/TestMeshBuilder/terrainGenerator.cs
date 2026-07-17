public class TerrainGenerator
{
	private NoiseProvider noise;

	public TerrainGenerator(NoiseProvider n)
	{
		noise = n;
	}

	public void Generate(WorldGrid grid)
	{
		for (int x = 0; x < grid.SizeX; x++)
		{
			for (int z = 0; z < grid.SizeZ; z++)
			{
				float h = noise.GetHeight(x, z);

				h = (float)System.Math.Pow((h + 1) * 0.5f, 2.5);

				grid.Height[x, z] = h * 50f;
			}
		}
	}
}
