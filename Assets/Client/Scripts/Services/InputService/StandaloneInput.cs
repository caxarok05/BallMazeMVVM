using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class StandaloneInput : AbstractInput
{
    public override bool GetInput()
    {
        return Input.GetMouseButtonDown(0);
    }

    public override Vector3 GetVector(bool cameraToScreenWorldPoint = false)
    {
        if (cameraToScreenWorldPoint)
        {
            return Camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.nearClipPlane));
        }

        return new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.nearClipPlane);
    }
}
