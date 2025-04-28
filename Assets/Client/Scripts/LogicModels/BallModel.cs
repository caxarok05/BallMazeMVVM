using Client.Scripts.LogicViews;
using Client.Scripts.Services;
using System;
using UniRx.Toolkit;
using UnityEngine;
using Zenject;

namespace Client.Scripts.LogicModels
{
    public class BallModel : IInitializable, IDisposable
    {
        private BallView _ballView;
        private BallCompressionView _compressionView;
        private LineView _lineView;
        private LineModel _lineModel;
        private BallRotationView _rotationView;

        public BallModel(
            BallView ballView,
            LineView lineView, 
            LineModel lineModel, 
            BallCompressionView compressionView, 
            BallRotationView rotationView)
        {
            _ballView = ballView;
            _lineView = lineView;
            _lineModel = lineModel;
            _compressionView = compressionView;
            _rotationView = rotationView;
        }
        public void Initialize()
        {
            _ballView.OnMouseClickedDown += ShowLine;
            _ballView.OnMouseClickedUp += HideLine;
            _ballView.RequestVelocity += OnVelocityRequested;
            _ballView.RequestRotation += SetRotation;
            _ballView.ChangeRotationVector += SetRotationVector;
            _ballView.HitBorder += OnWallHit;
            _lineView.RequestBallPosition += SetBallPosition;
        }
        public void Dispose()
        {
            _ballView.OnMouseClickedDown -= ShowLine;
            _ballView.OnMouseClickedUp -= HideLine;
            _ballView.RequestVelocity -= OnVelocityRequested;
            _ballView.RequestRotation -= SetRotation;
            _ballView.ChangeRotationVector -= SetRotationVector;
            _ballView.HitBorder -= OnWallHit;
            _lineView.RequestBallPosition -= SetBallPosition;
        }

        public void SetBallPosition()
        {
            _lineView.SetBallPosition(_ballView.BallPosition);
        }

        private void OnVelocityRequested(Vector3 currentPosition, Vector3 previousPosition)
        {
            _ballView.SetVelocity(CustomVelocity.GetVectorDiff(currentPosition, previousPosition));
        }

        public void SetVelocity(Vector3 target, Vector3 current)
        {
            _ballView.SetVelocity(_lineModel.CalculateLineVelocity(current, target));
        }

        public void SetRotation(Vector3 velocity)
        {
            _rotationView.Rotate(velocity);
        }

        public void SetRotationVector(Vector3 direction)
        {
            _rotationView.SetRotationVector(direction);
        }

        public void ShowLine()
        {
            _lineView.SetLineOn();
        }

        public void HideLine()
        {
            _lineView.SetLineOff();
        }

        private void OnWallHit(Vector3 ballVelocity, Vector3 contactPointNormal)
        {
            _compressionView.TryCompress();
            Vector3 reflectedVelocity = CustomVelocity.ReflectVector(ballVelocity, contactPointNormal);
            _ballView.SetVelocity(reflectedVelocity);
            _rotationView.SetRotationVector(reflectedVelocity);
            
        }
    }
}
