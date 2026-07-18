using System;

class Program
{
    static void Main()
    {
        float meanAltitude = 2000f;
        float standardDeviation = 100f;

        int regionSeed = 84897;


        IDistribution[] distributions =
        {
            new NormalDistribution(
                meanAltitude,
                standardDeviation
            ),

            new GammaDistribution(
                1700f,
                100f
            ),

            new UniformDistribution(
                1200f,
                50f
            )
        };


        foreach (IDistribution distribution in distributions)
        {
            float altitude = distribution.Sample(regionSeed);

            Console.WriteLine("-------------------------");
            Console.WriteLine(
                distribution.GetType().Name
            );

            Console.WriteLine(
                $"Mean: {distribution.Mean}"
            );

            Console.WriteLine(
                $"StdDev: {distribution.StandardDeviation}"
            );

            Console.WriteLine(
                $"Generated altitude: {altitude:F2} m"
            );
        }
    }
}