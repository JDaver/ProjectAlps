# Region Selection

The region selection stage generates the individual `RegionNode` instances that will compose the world graph.

The process is driven by the selected **Archetype** and the available **SubRegion definitions**.

```text id="b6q1sl"
Archetype
    │
    ├── NumberRegions
    └── ElevationProfile
            │
            ▼
    Elevation Targets
            │
            ▼
    SubRegion Selection
            │
            ▼
    Altitude Sampling
            │
            ▼
    RegionNode Collection
```

## SubRegion Definition

SubRegions are defined through JSON configuration files.

```json id="7myx4h"
{
  "RegionTypeId": 1,
  "Name": "Forest1",

  "OccurrenceRules": {
    "Min": 1,
    "Max": 4
  },

  "GenerationRules": {
    "MinExtent": 40,
    "MaxExtent": 40,
    "Elevation": {
      "DistributionType": "Normal",
      "Mean": 900.0,
      "StandardDeviation": 100.0
    }
  },

  "LinkRules": {
    "MinLinks": 1,
    "MaxLinks": 3,
    "Neighbours": [1, 2, 3, 4, 5]
  },

  "PropertiesRules": {
    "category": "forest",
    "isStart": false,
    "isEnd": true
  }
}
```

The main rule groups are:

* **OccurrenceRules** — controls how many times a SubRegion type may occur.
* **GenerationRules** — defines generation parameters, including its elevation distribution.
* **LinkRules** — defines which region types may be connected. These rules are used during graph generation.
* **PropertiesRules** — contains additional semantic properties used by later generation stages.

## Elevation Targets

The archetype provides an `ElevationProfile` and the number of regions to generate.

The profile is evaluated to produce one elevation target for each region:

```text id="n3zv6m"
NumberRegions = 6

Elevation Profile
       │
       ▼
[Target₀, Target₁, Target₂, Target₃, Target₄, Target₅]
```

The profile function and the archetype's minimum and maximum elevations determine the general elevation progression.

A small deterministic random perturbation is applied to each step, allowing different seeds to produce variations while preserving the overall profile.

These values are **targets**, not the final elevations of the regions.

## SubRegion Selection

For each elevation target, the system searches for a compatible SubRegion:

```csharp id="v2x6w9"
loader.FindClosestRule(
    target,
    regionOccurrences,
    maxTargetDistance
);
```

The selection considers:

* the target elevation;
* the number of times each SubRegion has already been selected;
* the maximum allowed distance from the target elevation.

The occurrence counter ensures that the configured `Min` and `Max` occurrence constraints can be respected.

## Altitude Sampling

Once a SubRegion has been selected, its elevation distribution is used to generate the actual altitude of the `RegionNode`.

For example:

```json id="o3v8bx"
"Elevation": {
  "DistributionType": "Normal",
  "Mean": 900.0,
  "StandardDeviation": 100.0
}
```

The current implementation samples from a normal distribution:

```text id="9z8n4c"
N(Mean, StandardDeviation)
```

This creates a distinction between the two elevation levels:

```text id="0d2x7j"
Archetype Elevation Profile
        │
        ▼
   Elevation Target
        │
        ▼
    SubRegion
        │
        ▼
 Sampled Region Elevation
```

The archetype therefore defines the **large-scale elevation progression**, while the SubRegion defines the **local elevation distribution**.

## RegionNode Generation

After selecting the SubRegion and sampling its altitude, a `RegionNode` is created with:

* a unique node ID;
* the `RegionTypeId`;
* the SubRegion name;
* the sampled elevation.

All generated nodes are stored in the `RegionsCollection`.

At the end of this stage, the world has a collection of concrete regions with their initial properties and elevations.

The regions are not connected yet. Their connections are established in the next stage.

→ [World Graph Generation](world-graph.md)
