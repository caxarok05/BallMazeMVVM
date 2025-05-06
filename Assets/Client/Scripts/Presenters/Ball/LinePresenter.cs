using Client.Scripts.Services;
using UnityEngine;
using Zenject;

namespace Client.Scripts.Presenters
{
    [RequireComponent(typeof(LineRenderer))]
    public class LinePresenter : MonoBehaviour
    {

        private Vector3 _ballPosition;
        private LineRenderer _line;
        private AbstractInput _inputService;
        private GameBallPresenter _ballPresenter;

        [Inject]
        public void Construct(AbstractInput input, GameBallPresenter ballPresenter)
        {
            _inputService = input;
            _ballPresenter = ballPresenter;
        }

        private void Start()
        {
            _line = GetComponent<LineRenderer>();
        }

        private void Update()
        {
            if (_inputService.GetInput())
            {
                SetBallPosition(_ballPresenter.BallPosition);
                _line.SetPosition(0, _ballPosition);
                _line.SetPosition(1, _inputService.GetVector(true));
            }
        }

        public void SetBallPosition(Vector3 position) => _ballPosition = position;

        public void SetLineOn() => gameObject.SetActive(true);
        public void SetLineOff()
        {
            gameObject.SetActive(false);
            ResetLine();
        }

        private void ResetLine()
        {
            _line.SetPosition(0, Vector3.zero);
            _line.SetPosition(1, Vector3.zero);
        }
    }
}
