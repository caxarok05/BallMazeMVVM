using UnityEngine;

public interface IInputService
{
    bool GetInput();
    Vector3 GetVector(bool cameraToScreenWorldPoint);
}