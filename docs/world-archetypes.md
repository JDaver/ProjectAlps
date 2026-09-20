# World Archetypes

An **Archetype** defines the high-level structure and constraints of a generated world.

It does not directly generate terrain. Instead, it provides the rules that control the subsequent generation stages, particularly:

* how many regions the world should contain;
* how regions may be connected;
* how elevation should be distributed across the world.

The archetype is therefore the first layer of abstraction in the procedural generation pipeline.

```text
Seed
  │
  ▼
Archetype Selection
  │
  ├── Number of Regions
  ├── Connectivity Rules
  └── Elevation Profile
          │
          ▼
    Region Generation
          │
          ▼
      World Graph
```

## Archetype Definition

Archetypes are defined through JSON configuration files.

A single archetype is described by four main components:

```text
Archetype
├── Id
├── Name
├── RegionRules
├── ConnettivityRules
└── ElevationProfile
```

For example:

```json
{
  "Id": 0,
  "Name": "VerticalClimb",

  "RegionRules": {
    "MinRegions": 6,
    "MaxRegions": 9
  },

  "ConnettivityRules": {
    "MinLinksPerNode": 1,
    "MaxLinksPerNode": 2,
    "BranchingPreference": 0.3,

    "AllowLoops": false,
    "LoopsProbability": 0
  },

  "ElevationProfile": {
    "Type": "Ascending",
    "Function": "Quadratic",
    "Min": 800,
    "Max": 2700,
    "Weight": 0.8
  }
}
```

## Archetype Instance

The JSON file describes the **rules** for an archetype. During world generation, these rules are used to create an `ArchetypeInstance`.

```csharp
public class ArchetypeInstance
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public int NumberRegions { get; private set; }

    public ConnettivityRules Connettivity { get; private set; }
    public ElevationProfile ElevationProfile { get; private set; }

    public ArchetypeInstance(ArchetypeLoader collection, int seed)
    {
        Random rng = new(seed);

        int chosenArchetypeId =
            GetRandomInRange(0, collection.Types.Length - 1, rng);

        WorldArchetypeRules chosenArchetypeRule =
            collection.Types[chosenArchetypeId];

        Id = chosenArchetypeId;
        Name = chosenArchetypeRule.Name;

        NumberRegions = GetRandomInRange(
            chosenArchetypeRule.RegionRules.MinRegions,
            chosenArchetypeRule.RegionRules.MaxRegions,
            rng
        );

        Connettivity = chosenArchetypeRule.ConnettivityRules;
        ElevationProfile = chosenArchetypeRule.ElevationProfile;
    }
}
```

The instance represents the **concrete archetype selected for the current world generation**.

The seed is used to make this selection deterministic. Given the same collection of archetypes and the same seed, the same archetype and region count will be selected.

### Archetype Selection

The generator first selects one of the available archetypes:

```text
ArchetypeLoader
      │
      ├── VerticalClimb
      ├── MountainRange
      └── Plateau
             │
             ▼
       Random Selection
             │
             ▼
      ArchetypeInstance
```

The selection is performed using the supplied seed.

This means that the world generation remains deterministic while still allowing different seeds to produce different world configurations.

## Region Rules

`RegionRules` determines the number of regions that will compose the world.

```json
"RegionRules": {
  "MinRegions": 6,
  "MaxRegions": 9
}
```

The generator selects an integer in the inclusive range:

```text
MinRegions ≤ NumberRegions ≤ MaxRegions
```

For the example above, the generated world can contain between **6 and 9 regions**.

The archetype therefore does not specify an exact number of regions. Instead, it defines a valid range from which the generator can choose.

The selected value is stored in:

```csharp
NumberRegions
```

This value is later used by the region and graph generation stages.

## Connectivity Rules

`ConnettivityRules` defines the topological constraints of the world graph.

```json
"ConnettivityRules": {
  "MinLinksPerNode": 1,
  "MaxLinksPerNode": 2,

  "BranchingPreference": 0.3,

  "AllowLoops": false,
  "LoopsProbability": 0
}
```

These rules do not determine the exact graph. They constrain the possible connections between regions.

### Links Per Node

```json
"MinLinksPerNode": 1,
"MaxLinksPerNode": 2
```

These values define the allowed range for the number of connections associated with a region.

Conceptually:

```text
MinLinksPerNode ≤ Links(node) ≤ MaxLinksPerNode
```

The actual graph is generated later using these constraints.

### Branching Preference

```json
"BranchingPreference": 0.3
```

`BranchingPreference` controls the tendency of the graph generation algorithm to create branching structures.

It is a preference rather than a strict structural constraint.

This allows different worlds generated from the same archetype to have different graph structures while remaining within the general characteristics defined by the archetype.

### Loops

```json
"AllowLoops": false,
"LoopsProbability": 0
```

`AllowLoops` determines whether the generated graph may contain cycles.

When loops are allowed, `LoopsProbability` controls the probability associated with creating them.

For example:

```text
AllowLoops = false

    A
   / \
  B   C
       \
        D
```

versus a graph where an additional connection may create a cycle:

```text
    A
   / \
  B---C
       \
        D
```

The exact interpretation of `LoopsProbability` belongs to the graph-generation algorithm and is therefore documented in the [World Graph](world-graph.md) documentation.

## Elevation Profile

`ElevationProfile` describes the large-scale elevation behavior associated with the archetype.

```json
"ElevationProfile": {
  "Type": "Ascending",
  "Function": "Quadratic",
  "Min": 800,
  "Max": 2700,
  "Weight": 0.8
}
```

The elevation profile is intentionally defined at the archetype level.

It describes the expected **large-scale elevation trend** of the generated world rather than the detailed terrain surface.

### Type

```json
"Type": "Ascending"
```

`Type` describes the general direction or behavior of elevation across the generated world.

Possible types may include, depending on the available configuration:

```text
Ascending
Descending
Flat
...
```

For example, an `Ascending` profile represents a world in which elevation tends to increase according to the spatial progression established during generation.

### Function

```json
"Function": "Quadratic"
```

`Function` specifies the mathematical function used to model the elevation profile.

In this example, the elevation follows a quadratic progression rather than a linear one.

The function operates on the large-scale structure of the world. It is not the same thing as the noise used later to generate local terrain variation.

### Minimum and Maximum Elevation

```json
"Min": 800,
"Max": 2700
```

These values define the elevation range used by the profile.

Conceptually:

```text
Min ≤ Elevation ≤ Max
```

For this archetype:

```text
800m ≤ Elevation ≤ 2700m
```

The exact elevation assigned to each region depends on the elevation function and the position or progression determined during the later generation stages.

### Weight

```json
"Weight": 0.8
```

`Weight` represents the influence of the elevation profile when determining the final elevation characteristics.

Its exact application depends on the elevation-generation algorithm.

## Example: VerticalClimb

The following configuration describes the `VerticalClimb` archetype:

```json
{
  "Id": 0,
  "Name": "VerticalClimb",

  "RegionRules": {
    "MinRegions": 6,
    "MaxRegions": 9
  },

  "ConnettivityRules": {
    "MinLinksPerNode": 1,
    "MaxLinksPerNode": 2,
    "BranchingPreference": 0.3,
    "AllowLoops": false,
    "LoopsProbability": 0
  },

  "ElevationProfile": {
    "Type": "Ascending",
    "Function": "Quadratic",
    "Min": 800,
    "Max": 2700,
    "Weight": 0.8
  }
}
```

This archetype therefore describes a world with:

* a variable number of regions between 6 and 9;
* relatively limited connectivity between regions;
* a preference for some branching;
* no graph loops;
* an overall ascending elevation profile;
* elevations ranging from 800m to 2700m;
* a quadratic elevation function.

These properties define the **constraints and tendencies** of the world. They do not uniquely determine its final structure.

Different seeds can therefore produce different worlds while still belonging to the same archetype.

## Role in the Generation Pipeline

The archetype is the first procedural decision made after the seed is initialized.

```text
Seed
 │
 ▼
Archetype Selection
 │
 ▼
ArchetypeInstance
 │
 ├── NumberRegions
 ├── Connectivity
 └── ElevationProfile
        │
        ▼
   Region Generation
        │
        ▼
   World Graph
```

The archetype establishes the rules that subsequent stages use to construct the world.

The next stage is the generation and selection of the individual regions that will populate the archetype.

→ [Region Selection](region-selection.md)
