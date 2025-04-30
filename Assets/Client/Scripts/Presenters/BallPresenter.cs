using Client.Scripts.Infrastructure.Signals;
using Client.Scripts.Services;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Client.Scripts.Presenters
{
    public class BallPresenter : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public Vector3 BallPosition { get; private set; }

        private Vector3 _ballVelocity;
        private Vector3 _previousPosition;

        private Ray _ray;
        private RaycastHit _raycastHit;
        private Camera _camera;
        private BallRotationPresenter _ballRotationPresenter;
        private AbstractInput _inputManager;
        private CustomPool<BorderParticleWrapper> _pool;
        private SignalBus _signalBus;

        private const int BorderLayer = 6;

        [Inject]
        public void Construct(AbstractInput input, CustomPool<BorderParticleWrapper> customPool, SignalBus signalBus, BallRotationPresenter ballRotationPresenter)
        {
            _inputManager = input;
            _pool = customPool;
            _signalBus = signalBus;
            _ballRotationPresenter = ballRotationPresenter;
        }

        private void Awake()
        {
            _camera = Camera.main;
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

        private async void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == BorderLayer)
            {
                ContactPoint contactPoint = collision.contacts[0];
                _signalBus.TryFire(new HitBorder(_ballVelocity, contactPoint.normal));

                BorderParticleWrapper particle = _pool.Get();
                particle.gameObject.transform.SetPositionAndRotation(contactPoint.point, Quaternion.LookRotation(contactPoint.normal));
                particle.GetComponent<ParticleSystem>().Play();
                await ReleaseParticle(particle);
            }
        }

        public async UniTask ReleaseParticle(BorderParticleWrapper particle)
        {
            await UniTask.WaitForSeconds(particle.GetComponent<ParticleSystem>().main.duration);
            _pool.Release(particle);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _signalBus.TryFire<OnBallClickedDown>();
            BallPosition = gameObject.transform.position;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _signalBus.TryFire<OnBallClickedUp>();
            _ray = _camera.ScreenPointToRay(_inputManager.GetVector());

            if (Physics.Raycast(_ray.origin, _ray.direction, out _raycastHit, Mathf.Infinity))
            {
                Vector3 lineVelocity = _raycastHit.point - transform.position;
                _ballVelocity = new Vector3(lineVelocity.x, 0f, lineVelocity.z);
            }
            _ballRotationPresenter.SetRotationVector(_ballVelocity);
        }

        public void SetVelocity(Vector3 velocity) => _ballVelocity = velocity;
    }
}
