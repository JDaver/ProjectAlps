using Godot;

public partial class WorldGenerator : Node3D
{
	[Export] public int SizeX = 400;
	[Export] public int SizeZ = 400;
	[Export] public int Seed = 464279;

	// collegato da Inspector (NON più GetNode)
	[Export] public MeshInstance3D TerrainMesh;

	private WorldGrid grid;
	private NoiseProvider noise;
	private TerrainGenerator terrain;
	private MeshBuilder meshBuilder;

	public override void _Ready()
	{

		if (TerrainMesh == null)
		{
			GD.PrintErr("TerrainMesh NON assegnato nell'Inspector!");
			return;
		}

		GenerateWorld();

		GD.Print("Mesh finale: " + TerrainMesh.Mesh);
		
	}

	public void GenerateWorld()
	{
		// 1. grid
		grid = new WorldGrid(SizeX, SizeZ);

		// 2. noise
		noise = new NoiseProvider(Seed);

		// 3. terrain logic
		terrain = new TerrainGenerator(noise);
		terrain.Generate(grid);
		
	// 4. mesh build
meshBuilder = new MeshBuilder();
Mesh mesh = meshBuilder.Build(grid);

// 5. assign mesh
TerrainMesh.Mesh = mesh;
	
	}
}
