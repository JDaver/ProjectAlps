using System;
using Distribution;

namespace Distribution
{
    public class GammaDistribution : IDistribution
{
    public float Mean { get; private set; }

    public float StandardDeviation { get; private set; }

    public float Variance => StandardDeviation * StandardDeviation;
    
    private float Shape => (Mean * Mean) / Variance;

    private float Scale => Variance / Mean;

    public GammaDistribution(float mean, float standardDeviation){
        Mean = mean;
        StandardDeviation = standardDeviation;
    }

    public float Sample(int seed){
        Random rng = new(seed);

        float k = Shape;
        float theta = Scale;

        return SampleGamma(rng, k) * theta;
    }
    

    private float SampleGamma(Random rng, float shape){
    if (shape < 1f)
    {
        float u = rng.NextSingle();

        return SampleGamma(rng, shape + 1f) *
               MathF.Pow(u, 1f / shape);
    }

    float d = shape - 1f / 3f;
    float c = 1f / MathF.Sqrt(9f * d);

    while (true)
    {
        float x;
        float v;

        do
        {
            x = SampleNormal(rng);
            v = 1f + c * x;
        }
        while (v <= 0);

        v = v * v * v;

        float u = rng.NextSingle();

        if (u < 1f - 0.0331f * x * x * x * x)
            return d * v;

        if (MathF.Log(u) < 
            0.5f * x * x + d * (1 - v + MathF.Log(v)))
            return d * v;
    }
} 

private float SampleNormal(Random rng)
{
    float u1 = rng.NextSingle();
    float u2 = rng.NextSingle();

    return MathF.Sqrt(-2f * MathF.Log(u1)) *
           MathF.Cos(2f * MathF.PI * u2);
}

}
}
