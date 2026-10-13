**World Matrix Discretization & Region Filling**

WorldMatrixDiscretization converts the generated 2D region positions into a discrete grid, calculating its dimensions, applying padding and resolution, and storing each region's ID and initial coordinates.

RegionFilling uses a multi-source, queue-based flood-fill algorithm to expand regions into empty cells. The expansion extents vary according to each cell's position within the grid.

**_WorldMatrixDiscretization_**
`WorldMatrixDiscretization` converts the generated 2D positions of region nodes into a discrete integer matrix.

The `CalculateSize()` method determines the minimum and maximum coordinates along both axes to calculate the required matrix dimensions. A fixed resolution (`RESOLUTION`) controls the mapping scale, while padding (`PADDING`) adds a margin around the generated regions.

The matrix is initialized with `-1`, representing empty cells. The `DiscretizePositions()` method then maps each region's coordinates to integer grid positions, stores its ID in `WorldMatrix`, and records the corresponding coordinates in `keyValuePairs`.

The `PrintMatrix()` method provides a simple textual representation of the grid for debugging purposes.

**_RegionFilling_**

`RegionFilling` implements a multi-source, queue-based flood-fill algorithm to assign empty cells in the discretized world matrix to existing regions.

Initially, each region is assigned a queue containing its starting coordinates. The algorithm processes these queues iteratively, expanding each region into neighbouring cells that are still empty (`EMPTY_CELL = -1`).

The region expansion is performed using a queue-based flood-fill algorithm. For each processed cell, the algorithm calculates a dynamic neighbourhood defined by the North, South, East, and West extents, which depend on the cell's position within the matrix and a fixed `aspectRatio` coefficient. Empty cells within this neighbourhood are assigned to the current region and enqueued for subsequent expansion. The process continues until all queues are empty or no further cells can be assigned.

## Output Examples

The following images represent one example of output for each archetype.

### VerticalClimb

![VerticalClimb output](docs/images/VerticalClimbRegionFillingEx.png)

### Plateau

![Plateau output](docs/images/PlateauRegionFillingEx.png)

### RollingHills

![RollingHills output](docs/images/RollingHillsRegionFillingEx.png)

> **Note:** These examples represent the current output of each archetype. The generation and region-filling implementations are still under development, so the results may change.
