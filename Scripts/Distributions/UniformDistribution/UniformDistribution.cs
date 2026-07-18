public class UniformDistribution : IDistribution
{
    // f(x) = 1/(b - a) for a <= x <= b, 0 otherwise
    
    public float Mean { get; private set; }

    public float StandardDeviation { get; private set; }

    public float Variance => StandardDeviation * StandardDeviation;

    public UniformDistribution(float mean, float standardDeviation){
        Mean = mean;
        StandardDeviation = standardDeviation;
    }
    
    private float Min =>
        Mean - (StandardDeviation * MathF.Sqrt(12f) / 2f);

    private float Max =>
        Mean + (StandardDeviation * MathF.Sqrt(12f) / 2f);


    public float Sample(int seed)
    {
        Random rng = new(seed);

        return rng.NextSingle() * (Max - Min) + Min;
    }
}


    