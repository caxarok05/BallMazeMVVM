using Client.Scripts.Infrastructure.Signals;
using Client.Scripts.Services;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using Zenject;

namespace Client.Scripts.Presenters
{
    public class MenuBallPresenter : MonoBehaviour
    {
        public Vector3 BallPosition { get; private set; }

        private Vector3 _ballVelocity;
        private Vector3 _previousPosition;

        private BallRotationPresenter _ballRotationPresenter;

        private CustomPool<BorderParticleWrapper> _pool;
        private SignalBus _signalBus;

        private const int BorderLayer = 6;

        [Inject]
        public void Construct(CustomPool<BorderParticleWrapper> customPool, SignalBus signalBus, BallRotationPresenter ballRotationPresenter)
        {
            _pool = customPool;
            _signalBus = signalBus;
            _ballRotationPresenter = ballRotationPresenter;
        }

        private void Awake()
        {
            _previousPosition = transform.position;
        }

        private void Update()
        {
            if (_ballVelocity != Vector3.zero)
            {
                transform.Translate(_ballVelocity * Time.deltaTime);
                SetVelocity(CustomVelocity.GetVectorDiff(transform.position, _previousPosition));
                _ballRotationPresenter.Rotate(_ballVelocity);
                _previousPosition = transform.position;
                BallPosition = transform.position;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == BorderLayer)
            {
                ContactPoint contactPoint = collision.contacts[0];
                _signalBus.TryFire(new HitBorder(_ballVelocity, contactPoint.normal));

                BorderParticleWrapper particle = _pool.Get();
                particle.gameObject.transform.SetPositionAndRotation(contactPoint.point, Quaternion.LookRotation(contactPoint.normal));
                particle.GetComponent<ParticleSystem>().Play();
                ReleaseParticle(particle, this.GetCancellationTokenOnDestroy()).Forget();
            }
        }

        public async UniTask ReleaseParticle(BorderParticleWrapper particle, CancellationToken sc = default)
        {
            await UniTask.WaitForSeconds(particle.GetComponent<ParticleSystem>().main.duration, ignoreTimeScale: false, PlayerLoopTiming.Update, sc);
            _pool.Release(particle);
        }

        public void SetVelocity(Vector3 velocity) => _ballVelocity = velocity;
    }
}
