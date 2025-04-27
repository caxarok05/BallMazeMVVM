using Unity.VisualScripting;
using UnityEngine;

public abstract class AbstractInput : IInputService, IInitializable
{
    protected Camera Camera { get; private set; }

    public void Initialize()
    {
        Camera = Camera.main;
    }

    public abstract bool GetInput();
    public abstract Vector3 GetVector(bool cameraToScreenWorldPoint);

}
