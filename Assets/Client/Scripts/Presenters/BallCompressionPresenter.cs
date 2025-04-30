using Cysharp.Threading.Tasks;
using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Client.Scripts.Presenters
{
    public class BallCompressionPresenter : MonoBehaviour
    {
        private const float MinStrengthVector = 0.01f;
        private const float MaxStrengthVector = 10;
        [SerializeField] private float _compressionSpeed = 5;
        private Vector3 _normalScale = Vector3.one;
        private Vector3 _maxCompressedScale = new Vector3(1, 1, 0.2f);

        public async void TryCompress(Vector3 velocity)
        {
            if (gameObject.transform.localScale == _normalScale)
            {
                await CompressBall(velocity);
            }
        }

        //need more info uni task 
        //zero allocation? what tf is that
        public async UniTask CompressBall(Vector3 velocity)
        {
            Vector3 compression = CustomVelocity.CalculateCompressionStrength(
                velocity, 
                MinStrengthVector, 
                MaxStrengthVector, 
                _normalScale, 
                _maxCompressedScale
            );

            while (transform.localScale != compression)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, compression, Time.deltaTime * _compressionSpeed);
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
}