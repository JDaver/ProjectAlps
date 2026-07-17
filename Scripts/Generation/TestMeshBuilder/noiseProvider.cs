using Godot;

public class NoiseProvider
{
	private const int FractalOctaves = 5;
private const float FractalLacunarity = 2.0f;
private const float FractalGain = 0.5f;
private const float FractalWeightedStrength = 0.8f;
	private FastNoiseLite noise;

	public NoiseProvider(int seed)
	{
		noise = new FastNoiseLite();
		noise.Seed = seed;

		noise.NoiseType = FastNoiseLite.NoiseTypeEnum.Simplex;

		noise.Frequency = 0.012f;
		
		noise.FractalOctaves = FractalOctaves;
		noise.FractalLacunarity = FractalLacunarity;
		noise.FractalGain = FractalGain;
		noise.FractalWeightedStrength = FractalWeightedStrength;
	}

	public float GetHeight(float x, float z)
	{
		return noise.GetNoise2D(x, z);
	}
}
