using UnityEngine;

namespace Client.Scripts.Infrastructure.Signals
{
    public class ChangeRotationVector
    {
        public Vector3 direction { get; private set; }
        public ChangeRotationVector(Vector3 direction)
        {
            this.direction = direction;
        }

    }
}
