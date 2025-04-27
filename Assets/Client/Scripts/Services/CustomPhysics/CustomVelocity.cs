using System.Numerics;

public class CustomVelocity
{
    public Vector3 GetVectorDiff(Vector3 start, Vector3 finish)
    {
        return Vector3.Subtract(start, finish);
    }

    public Vector3 ReflectVector(Vector3 vector, Vector3 normal)
    {
        return Vector3.Reflect(vector, normal);
    }
}
