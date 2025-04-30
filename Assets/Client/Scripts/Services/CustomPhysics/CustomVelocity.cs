using System;
using UnityEngine;

public static class CustomVelocity
{
    private static float Interpolation = 0.002f;
    public static Vector3 GetVectorDiff(Vector3 currentPosition, Vector3 previousPosition)
    {
        Vector3 ballVelocity = (currentPosition - previousPosition) / Time.deltaTime;
        ballVelocity = Vector3.Lerp(ballVelocity, Vector3.zero, Interpolation);
        return ballVelocity;
    }

    public static Vector3 ReflectVector(Vector3 vector, Vector3 normal)
    {
        return Vector3.Reflect(vector, normal);
    }

    public static Vector3 CalculateCompressionStrength(Vector3 velocity, float minStrengthVector, float maxStrengthVector, Vector3 normal, Vector3 maxCompressed)
    {
        float compressionStrength = Math.Abs(Math.Max(velocity.x, velocity.z));
        float ratio = (compressionStrength - minStrengthVector) / maxStrengthVector;
        float clampedRatio = Mathf.Clamp01(ratio);
        Vector3 compression = Vector3.Lerp(normal, maxCompressed, clampedRatio);
        return compression;
    }
}
