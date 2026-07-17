# Project Alps

Project Alps is a procedural world generation project developed in Godot using C#.
This project is developed in my spare time. Progress may be slow, but the goal is to steadily improve the architecture and procedural generation systems. 

The goal is to generate believable Alpine environments by combining graph-based world generation with deterministic procedural terrain generation. Instead of relying solely on noise functions, the world is first described through high-level geographical structures and then refined into terrain.

## World Generation Pipeline

```
Seed
  │
  ▼
World Graph
  │
  ├── Regions
  ├── Mountains
  ├── Valleys
  └── Forests
  │
  ▼
Fractal Noise
  │
  ▼
Terrain Heightmap
  │
  ▼
Biomes & Environment
  │
  ▼
Chunks & Mesh Generation
```

The graph defines the large-scale structure of the world, while fractal noise is used to add local terrain detail without breaking the coherence imposed by the graph.

> 🚧 The project is currently in its early development phase and the architecture is expected to evolve.
