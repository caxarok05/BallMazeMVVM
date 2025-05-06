using Client.Scripts.Infrastructure.Signals;
using Client.Scripts.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Client.Scripts.Presenters
{
    public abstract class BaseBallPresenter : MonoBehaviour, IBallPresenter
    {
        public Vector3 BallPosition { get; protected set; }

        protected Vector3 _ballVelocity;
        protected Vector3 _previousPosition;

        protected BallRotationPresenter _ballRotationPresenter;
        protected CustomPool<BorderParticleWrapper> _pool;
        protected ParticleBehaviourService _particleBehaviour;
        protected SignalBus _signalBus;

        private const int BorderLayer = 6;

        [Inject]
        public virtual void Construct(CustomPool<BorderParticleWrapper> customPool, SignalBus signalBus, BallRotationPresenter ballRotationPresenter, ParticleBehaviourService particleBehaviour)
        {
            _pool = customPool;
            _signalBus = signalBus;
            _ballRotationPresenter = ballRotationPresenter;
            _particleBehaviour = particleBehaviour;
        }

        protected virtual void Awake()
        {
            _previousPosition = transform.position;
        }

        protected virtual void Update()
        {
            if (_ballVelocity != Vector3.zero)
            {
                _ballRotationPresenter.Rotate(_ballVelocity);
            }
        }

        protected virtual void FixedUpdate()
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

        protected virtual void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == BorderLayer)
            {
                ContactPoint contactPoint = collision.contacts[0];
                _signalBus.TryFire(new HitBorder(_ballVelocity, contactPoint.normal));

                BorderParticleWrapper particle = _pool.Get();
                particle.gameObject.transform.SetPositionAndRotation(contactPoint.point, Quaternion.LookRotation(contactPoint.normal));
                particle.GetComponent<ParticleSystem>().Play();
                _particleBehaviour.ReleaseParticle(particle, _pool, this.GetCancellationTokenOnDestroy()).Forget();
            }
        }

        public void SetVelocity(Vector3 velocity) => _ballVelocity = velocity;
    }
}
