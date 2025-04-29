using Client.Scripts.Infrastructure.Signals;
using Client.Scripts.LogicViews;
using System;
using UnityEngine;
using Zenject;

namespace Client.Scripts.LogicModels
{
    public class BallModel : IInitializable, IDisposable
    {
        private readonly BallView _ballView;
        private readonly BallCompressionView _compressionView;
        private readonly LineView _lineView;
        private readonly BallRotationView _rotationView;
        private readonly SignalBus _signalBus;

        public BallModel(
            BallView ballView,
            LineView lineView,
            BallCompressionView compressionView,
            BallRotationView rotationView,
            SignalBus signalBus)
        {
            _ballView = ballView;
            _lineView = lineView;
            _compressionView = compressionView;
            _rotationView = rotationView;
            _signalBus = signalBus;
        }
        public void Initialize()
        {
            _signalBus.Subscribe<OnBallClickedDown>(ShowLine);
            _signalBus.Subscribe<OnBallClickedUp>(HideLine);
            _signalBus.Subscribe<RequestVelocity>(OnVelocityRequested);
            _signalBus.Subscribe<RequestRotation>(SetRotation);
            _signalBus.Subscribe<ChangeRotationVector>(SetRotationVector);
            _signalBus.Subscribe<HitBorder>(OnWallHit);
            _signalBus.Subscribe<RequestBallPosition>(SetBallPosition);
        }
        public void Dispose()
        {
            _signalBus.TryUnsubscribe<OnBallClickedDown>(ShowLine);
            _signalBus.TryUnsubscribe<OnBallClickedUp>(HideLine);
            _signalBus.TryUnsubscribe<RequestVelocity>(OnVelocityRequested);
            _signalBus.TryUnsubscribe<RequestRotation>(SetRotation);
            _signalBus.TryUnsubscribe<ChangeRotationVector>(SetRotationVector);
            _signalBus.TryUnsubscribe<HitBorder>(OnWallHit);
            _signalBus.TryUnsubscribe<RequestBallPosition>(SetBallPosition);
        }

        public void SetBallPosition()
        {
            _lineView.SetBallPosition(_ballView.BallPosition);
        }

        private void OnVelocityRequested(RequestVelocity args)
        {
            _ballView.SetVelocity(CustomVelocity.GetVectorDiff(args.currentPosition, args.previousPosition));
        }

        public void SetRotation(RequestRotation args)
        {
            _rotationView.Rotate(args.velocity);
        }

        public void SetRotationVector(ChangeRotationVector args)
        {
            _rotationView.SetRotationVector(args.direction);
        }

        public void ShowLine()
        {
            _lineView.SetLineOn();
        }

        public void HideLine()
        {
            _lineView.SetLineOff();
        }

        private void OnWallHit(HitBorder args)
        {
            _compressionView.TryCompress();
            Vector3 reflectedVelocity = CustomVelocity.ReflectVector(args.ballVelocity, args.contactPointNormal);
            _ballView.SetVelocity(reflectedVelocity);
            _rotationView.SetRotationVector(reflectedVelocity);
            
        }
    }
}
