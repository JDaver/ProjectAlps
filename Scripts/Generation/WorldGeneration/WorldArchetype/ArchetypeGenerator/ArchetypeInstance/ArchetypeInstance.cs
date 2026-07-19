using System;
using ProjectAlps.Generation.WorldGeneration.WorldArchetype.Rules;
using ProjectAlps.Generation.WorldGeneration.WorldArchetype.Loader;

namespace ProjectAlps.Generation.WorldGeneration.WorldArchetype.Instance;

public class ArchetypeInstance{ 

    public int Id {get; private set;}

    public string Name {get; private set;}

    public int NumberRegions {get; private set;} 

    public ConnettivityRules Connettivity {get; private set;}

    public ElevationProfile ElevationProfile {get; private set;}

    public ArchetypeInstance(ArchetypeLoader collection,int seed){
        Random rng = new(seed);
        //generate a number between 1 and 3
        int chosenArchetypeId = GetRandomInRange(1,collection.Types.Length - 1,rng);
        
        WorldArchetypeRules chosenArchetypeRule = collection.Types[chosenArchetypeId];

        Id = chosenArchetypeId;
        Name = chosenArchetypeRule.Name;

        NumberRegions = GetRandomInRange(chosenArchetypeRule.RegionRules.MinRegions,chosenArchetypeRule.RegionRules.MaxRegions, rng);

        Connettivity = chosenArchetypeRule.ConnettivityRules;
        ElevationProfile = chosenArchetypeRule.ElevationProfile;
    }

    private int GetRandomInRange(int min, int max, Random rng){
        return rng.Next(min, max + 1);
    }
}

