using System;

namespace ProjectAlps.Functions;

public static class Functions{

    // Cosine function: f(x) = [cos(4x) + 1]/2   
    // 0 < y < 1
    // 0 < x < pi
    public float Cosine(float x){
        float scaledX = 4f * (x * MathF.PI);
        return (MathF.Cos(scaledX) + 1f)/2f;
    }

    // Tanh function: f(x) = [tanh(3x) + 1]/2
    // 0 < y < 1
    // 0 < x < 1
    public float HyperbolicTan(float x){
        float scaledX = 3f * x;
        return (MathF.Tanh(scaledX) + 1f ) / 2f;
    }

    // Quadratic function: f(x) = 1/2 * (x^2 + x)
    // 0 < y < 1
    // 0 < x < 1 
    public float Quadratic(float x){
        float ScaledX = x/2;
        return ScaledX * ScaledX + ScaledX;
    }
}