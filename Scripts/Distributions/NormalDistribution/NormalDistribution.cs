public class NormalDistribution : IDistribution{
    
    // f(x) = (1 / (σ * sqrt(2π))) * e^(-(x - μ)^2 / (2σ^2))

    public float Mean { get; private set; }

    public float StandardDeviation { get; private set; }

    public float Variance => StandardDeviation * StandardDeviation;

        public NormalDistribution(float mean, float standardDeviation)
    {
        Mean = mean;
        StandardDeviation = standardDeviation;
    }

    public float Sample(int seed){
    Random rng = new Random(seed);

    float u1 = rng.NextSingle();
    float u2 = rng.NextSingle();
    float z = MathF.Sqrt(-2.0f * MathF.Log(u1)) *
              MathF.Cos(2.0f * MathF.PI * u2);

    return z * StandardDeviation + Mean;

    }
}