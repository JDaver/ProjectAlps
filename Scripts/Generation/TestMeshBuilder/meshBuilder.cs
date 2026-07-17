using Godot;

public class MeshBuilder
{
	public Mesh Build(WorldGrid grid)
	{
		var st = new SurfaceTool();
		st.Begin(Mesh.PrimitiveType.Triangles);

		for (int x = 0; x < grid.SizeX - 1; x++)
		{
			for (int z = 0; z < grid.SizeZ - 1; z++)
			{
				Vector3 v1 = new Vector3(x, grid.Height[x, z], z);
				Vector3 v2 = new Vector3(x + 1, grid.Height[x + 1, z], z);
				Vector3 v3 = new Vector3(x, grid.Height[x, z + 1], z + 1);
				Vector3 v4 = new Vector3(x + 1, grid.Height[x + 1, z + 1], z + 1);

				// tri 1
				st.AddVertex(v1);
				st.AddVertex(v2);
				st.AddVertex(v3);

				// tri 2
				st.AddVertex(v3);
				st.AddVertex(v2);
				st.AddVertex(v4);
			}
		}

		st.GenerateNormals();


		return st.Commit();
	}
}
