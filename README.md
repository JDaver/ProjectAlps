# Project Alps

Project Alps is a procedural world generation project developed in Godot using C#.

The project aims to generate believable Alpine environments through a deterministic, data-driven procedural generation pipeline. Instead of relying solely on noise functions, the world is first described through high-level geographical structures and relationships, which are then translated into spatial representations and, eventually, terrain.

The generation system is driven by **JSON configuration files**, which define world archetypes and the rules governing the generation of individual regions.

> 🚧 The project is currently in early development. The architecture and generation algorithms are expected to evolve as the project progresses.

## World Generation Pipeline

```text
Seed
  │
  ▼
World Archetype
  │
  ▼
Region Selection
  │
  ▼
World Graph
  │
  ▼
Graph → R² Spatial Representation
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

The generation process is divided into several stages. Each stage is responsible for a specific level of abstraction, allowing the world to be defined progressively from high-level geographical concepts to a concrete terrain representation.

### 1. Archetype Selection

The first stage determines the **archetype of the world** to generate.

An archetype represents a high-level geographical structure, such as a mountain range, plateau, or vertical-climbing environment. It defines the general characteristics and constraints that will guide the subsequent generation stages.

Archetypes are defined through JSON configuration files.

[Read more about World Archetypes →](docs/world-archetypes.md)

### 2. Region Selection

Once the archetype has been selected, the generation system determines which **regions** compose the world.

Regions represent the major geographical areas of the generated environment, such as valleys, forests, gorges, plateaus, or other terrain structures.

The archetype provides the rules and constraints used to determine which regions can coexist and how they may be connected.

[Read more about Region Selection →](docs/region-selection.md)

### 3. World Graph Construction

The selected regions are then organized into a **graph structure**.

Each region becomes a node, while connections between regions define the topological structure of the world. At this stage the world describes **which regions are connected to each other**, but not yet their exact geometric position in space.

The graph therefore acts as an intermediate representation between geographical logic and spatial generation.

[Read more about World Graph Generation →](docs/world-graph.md)

### 4. Graph → R² Spatial Representation

The generated graph must then be translated into a geometric representation in the two-dimensional space **R²**.

This stage assigns spatial coordinates to the regions while attempting to preserve the topology and structural relationships defined by the graph.

The resulting representation provides the spatial foundation required by the subsequent terrain and heightmap generation stages.

[Read more about Graph → R² Translation →](docs/graph-to-r2.md)

## Development Status

The current development focuses on the early stages of the procedural generation pipeline:

* [x] World archetype definition
* [x] Region rule definition
* [x] Region graph generation
* [ ] Graph → R² spatial representation
* [ ] Terrain heightmap generation
* [ ] Biome generation
* [ ] Environment generation
* [ ] Chunk and mesh generation

The project is developed in my spare time, so progress may be gradual. The goal is to progressively build a modular and deterministic procedural generation system while keeping the architecture flexible enough to support different world types.
