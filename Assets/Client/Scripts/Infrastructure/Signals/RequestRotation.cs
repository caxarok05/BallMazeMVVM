using UnityEngine;

namespace Client.Scripts.Infrastructure.Signals
{
    public class RequestRotation
    {
        public Vector3 velocity { get; private set; }

        public RequestRotation(Vector3 velocity)
        {
            this.velocity = velocity;
        }
    }
}
