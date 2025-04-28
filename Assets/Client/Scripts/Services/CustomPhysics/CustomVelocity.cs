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
}
