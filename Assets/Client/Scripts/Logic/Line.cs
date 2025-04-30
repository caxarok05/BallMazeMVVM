using UnityEngine;

namespace Client.Scripts.Logic
{
    public class Line
    {
        public Vector3 CalculateLineVelocity(Vector3 start, Vector3 end)
        {
            var velocity = end - start;
            return new Vector3(velocity.x, 0, velocity.z);
        }
    }
}
