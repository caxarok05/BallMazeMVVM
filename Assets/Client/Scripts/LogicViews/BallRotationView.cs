using System.Runtime.InteropServices;
using UnityEngine;

namespace Client.Scripts.LogicViews
{
    public class BallRotationView : MonoBehaviour
    {
        public void Rotate(Vector3 velocity)
        {
            float velocityX = Mathf.Abs(velocity.x);
            float velocityZ = Mathf.Abs(velocity.z);
            float mainVelocity;

            if (velocityX > velocityZ)
                mainVelocity = velocityX;
            else
                mainVelocity = velocityZ;

            transform.Rotate(new Vector3(mainVelocity, 0, 0), Space.Self);
        }

        public void SetRotationVector(Vector3 velocity)
        {
            transform.localRotation = Quaternion.LookRotation(new Vector3(velocity.x, transform.rotation.y, velocity.z));
        }
    }
}
