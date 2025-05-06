using System;
using UnityEngine;

public static class CustomVelocity
{
    private static float Interpolation = 0.01f;
    public static Vector3 GetVectorDiff(Vector3 currentPosition, Vector3 previousPosition)
    {
        Vector3 ballVelocity = (currentPosition - previousPosition) / Time.fixedDeltaTime;
        ballVelocity = Vector3.Lerp(ballVelocity, Vector3.zero, Interpolation);
        return ballVelocity;
    }

    public static Vector3 ReflectVector(Vector3 vector, Vector3 normal)
    {
        return Vector3.Reflect(vector, normal);
    }

    public static Vector3 CalculateCompressionStrength(Vector3 velocity, float minStrengthVector, float maxStrengthVector, Vector3 normal, Vector3 maxCompressed)
    {
        float compressionStrength = Math.Max(Math.Abs(velocity.x), Math.Abs(velocity.z));
        float ratio = (compressionStrength - minStrengthVector) / maxStrengthVector;
        float clampedRatio = Mathf.Clamp01(ratio);
        Vector3 compression = Vector3.Lerp(normal, maxCompressed, clampedRatio);
        return compression;
    }

    public static Vector3 CalculateLineVelocity(Vector3 start, Vector3 end)
    {
        var velocity = end - start;
        return new Vector3(velocity.x, 0, velocity.z);
    }

    public static bool CalculateEntryAngle(Transform originPoint, Vector3 targetPoint, float angleLimit)
    {
        Vector3 direction = (targetPoint - originPoint.position).normalized;
        Vector3 groundDirection = new(direction.x, 0, direction.z);
        if (Vector3.Angle(originPoint.forward, groundDirection) < angleLimit || Vector3.Angle(originPoint.forward, groundDirection) > 180 - angleLimit)
            return true;
        else
            return false;
    }
}
