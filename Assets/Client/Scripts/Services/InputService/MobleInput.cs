using UnityEngine;

namespace Client.Scripts.Services
{
    public class MobleInput : AbstractInput
    {
        public override bool GetInput()
        {
            return Input.touchCount > 0;
        }

        public override Vector3 GetVector(bool cameraToScreenWorldPoint = false)
        {
            Vector3 inputPosition = Input.GetTouch(0).position;
            if (cameraToScreenWorldPoint)
            {
                return Camera.ScreenToWorldPoint(new Vector3(inputPosition.x, inputPosition.y, Camera.nearClipPlane));
            }

            return new Vector3(inputPosition.x, inputPosition.y, Camera.nearClipPlane);
        }
    }
}