using System;
using System.Collections.Generic;

namespace ProjectAlps.Functions;
public static class FunctionRegistry
{
    private static readonly Dictionary<string, Func<float, float>> functions = new()
    {
        { "Cosine", Functions.Cosine },
        { "HyperbolicTan", Functions.HyperbolicTan },
        { "Quadratic", Functions.Quadratic }
    };


    public static float Evaluate(string functionName, float x)
    {
        if(functions.TryGetValue(functionName, out var function))
        {
            return function(x);
        }

        throw new Exception($"Unknown function: {functionName}");
    }
}