public class WorldSeed
{
    public int Value { get; }


    public WorldSeed(int value)
    {
        Value = value;
    }


    public int GetModuleSeed(string moduleName)
    {
        return HashCode.Combine(
            Value,
            moduleName
        );
    }
}