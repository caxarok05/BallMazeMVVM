using Cysharp.Threading.Tasks;
using UnityEngine;

public class BallCompressionView : MonoBehaviour
{
    private Vector3 normalScale = Vector3.one;
    private Vector3 compressedScale = new Vector3(1, 1, 1);

    public async void TryCompress()
    {
        if (gameObject.transform.localScale == normalScale)
            await CompressBall();
    }

    private async UniTask CompressBall()
    {
        while(transform.localScale != compressedScale)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, compressedScale, Time.deltaTime);
            await UniTask.Yield();
        }
        while (transform.localScale != normalScale)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, normalScale, Time.deltaTime);
            await UniTask.Yield();
        }
        transform.localScale = normalScale;
    }  
   
}

