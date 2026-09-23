# Graph to 2D Spatial Representation

> **Draft Notice**
> This document describes the current approach for transforming the generated region graph into a two-dimensional spatial representation.
>
> The pipeline is still under development. Individual stages, constraints, and transformations may change during implementation and testing.

## 1. Objective

The objective is to transform the generated region graph into a two-dimensional spatial representation suitable for subsequent world-generation stages.

Given a graph:

```math
G = (V,E)
```

where each vertex represents a region and each edge represents a connection between regions, the process assigns a continuous position in:

```math
R²
```

to each region.

The resulting spatial representation will subsequently be converted into a discrete map representation.

The transformation is intended to preserve the topology of the generated graph while allowing its spatial arrangement to be adapted to the characteristics of the world archetype.

---

## 2. Spatial Representation Pipeline

The current pipeline is divided into a sequence of stages:

```text
Region Graph
     │
     ▼
Initial Spatial Configuration
     │
     ▼
Fruchterman–Reingold Embedding
     │
     ▼
[ Future spatial transformations ]
     │
     ▼
[ Geometric validation ]
     │
     ▼
[ Discretization ]
     │
     ▼
[ Map construction ]
```

The stages marked as future work are intentionally left open and may be refined as the implementation evolves.

The continuous spatial representation is kept separate from the final discrete map so that geometric operations can be performed independently of the resolution of the final representation.

---

## 3. Initial Spatial Configuration

Before the force-directed embedding, each region is assigned an initial position in `R²`.

For a region:

```math
v ∈ V
```

its position is represented as:

```math
p(v) ∈ R²
```

The strategy used to generate the initial configuration is an implementation detail and may be modified during development.

The initial configuration does not represent the final spatial arrangement.

It provides the starting state for the subsequent embedding process.

---

## 4. Force-Directed Embedding

The current embedding strategy is based on the **Fruchterman–Reingold force-directed graph drawing algorithm**.

The method models the graph as a physical system. Nodes exert **repulsive forces** on one another, while connected nodes exert **attractive forces** on one another.

The resulting configuration is obtained through an iterative process in which the position of each node is repeatedly adjusted according to the forces acting upon it.

Conceptually:

```text
        Repulsion
     ↗      ↑      ↖
       [ Node ]
     ↘      ↓      ↙
        Repulsion

Connected nodes
        ↓
     Attraction
```

For a characteristic distance `k`, the original formulation defines the force magnitudes as:

```math
f_r(d) = k² / d
```

for repulsion, and:

```math
f_a(d) = d² / k
```

for attraction.

Therefore:

* nodes that are too close tend to move apart;
* connected nodes that are too far apart tend to move closer;
* the configuration progressively approaches a spatial equilibrium.

The algorithm does not modify the graph topology. It only determines a continuous position:

```math
p : V → R²
```

for each node.

The iterative process is controlled by parameters such as:

* initial spatial area;
* ideal distance `k`;
* number of iterations;
* maximum displacement;
* temperature;
* cooling strategy.

The exact parameterization remains subject to implementation and testing.

### Reference

For the original formulation and complete mathematical treatment:

**Fruchterman, T. M. J. & Reingold, E. M. (1991), *Graph Drawing by Force-directed Placement*, Software: Practice and Experience, 21(11), 1129–1164.**

* [Original paper — Wiley / DOI](https://doi.org/10.1002/spe.4380211102)
* [Original paper — PDF](https://reingold.co/force-directed.pdf)

---

## 5. Archetype-Dependent Spatial Transformation

The generic spatial configuration produced by the embedding may subsequently be modified according to the world archetype.

The purpose of this stage is to introduce large-scale geometric characteristics specific to the generated world.

For example, an archetype representing a predominantly vertical environment may require a different spatial distribution from an archetype representing a wide plateau.

The transformation operates on the spatial representation:

```math
p(v) ∈ R²
```

without modifying the underlying graph:

```math
G = (V,E)
```

More generally, the transformation can be represented as a function:

```math
T : R² → R²
```

so that:

```math
p'(v) = T(p(v))
```

The transformation is not necessarily restricted to a linear mapping.

Possible approaches include:

* **linear transformations**, such as scaling, rotation, shearing or combinations thereof;
* **non-linear transformations**, allowing more complex deformations of the spatial configuration;
* other archetype-specific transformations to be defined during development.

The exact transformations and their parameters are still under development.

---

## 6. Spatial Validation

After the embedding and any subsequent spatial transformations, the generated configuration will be evaluated against a set of geometric constraints.

Potential validation criteria include:

* overlapping regions;
* minimum distance between regions;
* invalid spatial configurations;
* undesirable edge intersections;
* boundaries of the available spatial domain.

The validation strategy and the possible correction procedures are still under development.

---

## 7. Discretization

Once the continuous spatial configuration has been validated, it can be converted into a discrete representation.

The continuous position of each region:

```math
p(v_i) = (x_i,y_i)
```

will be mapped to a discrete coordinate:

```math
(r_i,c_i)
```

representing a cell in the final map representation.

The discretization strategy, resolution, scaling and collision handling are currently under development.

The discrete representation is intentionally kept separate from the continuous embedding.

---

## 8. Map Construction

The discrete representation obtained from the previous stage provides the spatial anchors for the generated regions.

It does not necessarily represent the complete terrain.

Further processing may be required to fill the space between regions and generate the final world representation.

Possible future stages include:

* region expansion;
* neutral zones;
* region boundaries;
* terrain constraints;
* procedural terrain generation;
* heightmap generation.

These stages are outside the scope of the graph embedding itself.

---

## 9. Future Development

The spatial representation pipeline is expected to evolve as the implementation progresses.

Possible future additions include:

* archetype-specific spatial transformations;
* spatial constraints during the embedding;
* collision resolution;
* adaptive map scaling;
* region filling;
* neutral boundary generation;
* terrain-aware spatial constraints;
* integration with procedural heightmap generation.

The document will be updated as individual stages become sufficiently defined.

At the current stage, the **Fruchterman–Reingold embedding represents the core method for obtaining the initial continuous spatial arrangement of the region graph**, while the subsequent stages remain intentionally modular.
