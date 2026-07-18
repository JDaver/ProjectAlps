public interface IDistribution
{
    float Mean { get; }
    float Variance { get; }
    float StandardDeviation { get; }

    float Sample(int seed);
}