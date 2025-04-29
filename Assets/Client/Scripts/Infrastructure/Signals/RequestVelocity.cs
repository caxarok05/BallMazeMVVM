using UnityEngine;

namespace Client.Scripts.Infrastructure.Signals
{
    public class RequestVelocity
    {
        public Vector3 currentPosition { get; private set; }
        public Vector3 previousPosition { get; private set; }

        public RequestVelocity(Vector3 currentPosition, Vector3 previousPosition)
        {
            this.currentPosition = currentPosition;
            this.previousPosition = previousPosition;
        }
    }
}
