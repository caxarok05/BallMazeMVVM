using Client.Scripts.Infrastructure.Signals;
using Client.Scripts.Presenters;
using Client.Scripts.Services;
using UnityEngine;
using Zenject;

namespace Client.Scripts.Logic
{
    public class GameBall : BaseBall
    {
        private readonly GameBallPresenter _ballPresenter;
        private readonly LinePresenter _linePresenter;
        private readonly AbstractInput _inputManager;

        private RaycastHit _raycastHit;

        public GameBall(
           AbstractInput input,
           GameBallPresenter ballPresenter,
           LinePresenter linePresenter, 
           BallCompressionPresenter compressionPresenter,
           BallRotationPresenter rotationPresenter,
           SignalBus signalBus) : base(compressionPresenter, rotationPresenter, signalBus)
        {
            _ballPresenter = ballPresenter;
            _linePresenter = linePresenter;
            _inputManager = input;
        }

        public override void Initialize()
        {
            _signalBus.Subscribe<OnBallClickedDown>(ShowLine);
            _signalBus.Subscribe<OnBallClickedUp>(HideLine);
            _signalBus.Subscribe<HitBorder>(OnWallHit);
        }
        public override void Dispose()
        {
            _signalBus.TryUnsubscribe<OnBallClickedDown>(ShowLine);
            _signalBus.TryUnsubscribe<OnBallClickedUp>(HideLine);
            _signalBus.TryUnsubscribe<HitBorder>(OnWallHit);
        }

        public void ShowLine()
        {
            _linePresenter.SetLineOn();
        }

        public void HideLine()
        {
            FindLineVelocity();

            _linePresenter.SetLineOff();
        }

        private void FindLineVelocity()
        {
            var _ray = Camera.main.ScreenPointToRay(_inputManager.GetVector());

            if (Physics.Raycast(_ray.origin, _ray.direction, out _raycastHit, Mathf.Infinity))
            {
                _ballPresenter.SetVelocity(CustomVelocity.CalculateLineVelocity(_ballPresenter.BallPosition, _raycastHit.point));
            }
        }

        private void OnWallHit(HitBorder args)
        {
            base.OnWallHit(args, _ballPresenter);
        }

    }
}
