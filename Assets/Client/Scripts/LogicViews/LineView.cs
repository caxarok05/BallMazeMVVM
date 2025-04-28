using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Client.Scripts.LogicViews
{
    [RequireComponent(typeof(LineRenderer))]
    public class LineView : MonoBehaviour
    {
        public event UnityAction RequestBallPosition;

        private Vector3 _ballPosition;
        private LineRenderer _line;
        private AbstractInput _inputService;

        [Inject]
        public void Construct(AbstractInput input)
        {
            _inputService = input;
        }

        private void Start()
        {
            _line = GetComponent<LineRenderer>();
        }

        private void Update()
        {
            if (_inputService.GetInput())
            {
                RequestBallPosition?.Invoke();
                _line.SetPosition(0, _ballPosition);
                _line.SetPosition(1, _inputService.GetVector(true));
            }
        }

        public void SetBallPosition(Vector3 position) => _ballPosition = position;

        public void SetLineOn() => gameObject.SetActive(true);
        public void SetLineOff() => gameObject.SetActive(false);
    }
}
