using UnityEngine;

namespace Client.Scripts.Infrastructure.Signals
{
    public class HitBorder
    {
        public Vector3 ballVelocity { get; private set; }
        public Vector3 contactPointNormal { get; private set; }

        public HitBorder(Vector3 ballVelocity, Vector3 contactPointNormal)
        {
            this.ballVelocity = ballVelocity;
            this.contactPointNormal = contactPointNormal;
        }
    }
}
