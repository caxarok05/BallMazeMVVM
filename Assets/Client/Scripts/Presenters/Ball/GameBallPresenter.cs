using Client.Scripts.Infrastructure.Signals;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Client.Scripts.Presenters
{
    public class GameBallPresenter : BaseBallPresenter, IPointerDownHandler, IPointerUpHandler
    {
        protected override void Awake()
        {
            base.Awake();
        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            _signalBus.TryFire(new BallRotationChanged(Vector3.Angle(_ballVelocity, Vector3.right)));
            _signalBus.TryFire(new BallSpeedChanged(_ballVelocity.magnitude));
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            _signalBus.TryFire<OnBallClickedDown>();
            BallPosition = gameObject.transform.position;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _signalBus.TryFire<OnBallClickedUp>();
            _ballRotationPresenter.SetRotationVector(_ballVelocity);
        }

        public void SetBallNewPosition(Vector3 position)
        {
            BallPosition = position;
            _previousPosition = position;
        }
    }
}
