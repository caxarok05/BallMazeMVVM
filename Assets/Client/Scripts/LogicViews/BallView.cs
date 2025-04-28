using Client.Scripts.Services;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Client.Scripts.LogicViews
{
    public class BallView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {

        public event Action OnMouseClickedDown;
        public event Action OnMouseClickedUp;
        public event Action<Vector3, Vector3> RequestVelocity;
        public event Action<Vector3, Vector3> HitBorder;
        public event Action<Vector3> RequestRotation;
        public event Action<Vector3> ChangeRotationVector;
        public Vector3 BallPosition { get; private set; }

        private Vector3 _ballVelocity;
        private Vector3 _previousPosition;

        private Ray _ray;
        private RaycastHit _raycastHit;
        private Camera _camera;
        private AbstractInput _inputManager;
        private CustomPool<BorderParticleWrapper> _pool;
        
        private const int BorderLayer = 6;

        [Inject]
        public void Construct(AbstractInput input, CustomPool<BorderParticleWrapper> customPool)
        {
            _inputManager = input;
            _pool = customPool;
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
                RequestVelocity?.Invoke(transform.position, _previousPosition);
                RequestRotation?.Invoke(_ballVelocity);
                _previousPosition = transform.position;
                BallPosition = transform.position;
            }
        }

        private async void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == BorderLayer)
            {
                ContactPoint contactPoint = collision.contacts[0];
                HitBorder?.Invoke(_ballVelocity, contactPoint.normal);

                BorderParticleWrapper particle = _pool.Get();
                particle.gameObject.transform.position = contactPoint.point;
                particle.gameObject.transform.rotation = Quaternion.LookRotation(contactPoint.normal);
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
            OnMouseClickedDown?.Invoke();
            BallPosition = gameObject.transform.position;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnMouseClickedUp?.Invoke();
            _ray = _camera.ScreenPointToRay(_inputManager.GetVector());

            if (Physics.Raycast(_ray.origin, _ray.direction, out _raycastHit, Mathf.Infinity))
            {
                Vector3 lineVelocity = _raycastHit.point - transform.position;
                _ballVelocity = new Vector3(lineVelocity.x, 0f, lineVelocity.z);
            }
            ChangeRotationVector?.Invoke(_ballVelocity);
        }

        public void SetVelocity(Vector3 velocity) => _ballVelocity = velocity;
    }
}
