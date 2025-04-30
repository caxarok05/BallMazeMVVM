using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class BallCompressionView : MonoBehaviour
{
    [SerializeField] private float _compressionSpeed = 5;
    private Vector3 _normalScale = Vector3.one;
    private Vector3 _compressedScale = new Vector3(1, 1, 0.7f);

    public async void TryCompress()
    {
        if (gameObject.transform.localScale == _normalScale)
            await CompressBall();
    }

    //need more info uni task 
    //zero allocation? what tf is that
    private async UniTask CompressBall()
    {
        while(transform.localScale != _compressedScale)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, _compressedScale, Time.deltaTime * _compressionSpeed);
            await UniTask.Yield();
        }
        while (transform.localScale != _normalScale)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, _normalScale, Time.deltaTime * _compressionSpeed);
            await UniTask.Yield();
        }
        transform.localScale = _normalScale;
    }  
   
}

