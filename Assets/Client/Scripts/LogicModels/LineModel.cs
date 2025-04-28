using UnityEngine;

namespace Client.Scripts.LogicModels
{
    public class LineModel
    {
        public Vector3 CalculateLineVelocity(Vector3 start, Vector3 end)
        {
            var velocity = end - start;
            return new Vector3(velocity.x, 0, velocity.z);
        }
    }
}
