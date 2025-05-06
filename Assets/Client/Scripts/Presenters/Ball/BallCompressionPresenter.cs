using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Client.Scripts.Presenters
{
    public class BallCompressionPresenter : MonoBehaviour
    {
        [SerializeField] private float _compressionSpeed = 5;
        
        private Vector3 _normalScale = Vector3.one;
        private Vector3 _maxCompressedScale = new Vector3(1, 1, 0.2f);
        private UniTask _compressionTask;

        private const float MinStrengthVector = 0.01f;
        private const float MaxStrengthVector = 10;

        public void TryCompress(Vector3 velocity)
        {
            if (gameObject.transform.localScale == _normalScale && _compressionTask.Status.IsCompleted())
            {
                _compressionTask = CompressBall(velocity, this.GetCancellationTokenOnDestroy());
            }
        }
        private async UniTask CompressBall(Vector3 velocity, CancellationToken ct = default)
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
                await UniTask.Yield(ct);
            }
            while (transform.localScale != _normalScale)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, _normalScale, Time.deltaTime * _compressionSpeed);
                await UniTask.Yield(ct);
            }
            transform.localScale = _normalScale;
        }
    }
}