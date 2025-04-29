using UnityEngine;

public abstract class AbstractInput 
{
    protected Camera Camera { get; private set; }

    public void Init()
    {
        Camera = Camera.main;
    }

    public abstract bool GetInput();
    public abstract Vector3 GetVector(bool cameraToScreenWorldPoint = false);

}
