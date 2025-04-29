using Client.Scripts.Infrastructure.Signals;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Client.Scripts.LogicViews
{
    [RequireComponent(typeof(LineRenderer))]
    public class LineView : MonoBehaviour
    {

        private Vector3 _ballPosition;
        private LineRenderer _line;
        private AbstractInput _inputService;
        private SignalBus _signalBus;

        [Inject]
        public void Construct(AbstractInput input, SignalBus signalBus)
        {
            _inputService = input;
            _signalBus = signalBus;
        }

        private void Start()
        {
            _line = GetComponent<LineRenderer>();
        }

        private void Update()
        {
            if (_inputService.GetInput())
            {
                _signalBus.TryFire<RequestBallPosition>();
                _line.SetPosition(0, _ballPosition);
                _line.SetPosition(1, _inputService.GetVector(true));
            }
        }

        public void SetBallPosition(Vector3 position) => _ballPosition = position;

        public void SetLineOn() => gameObject.SetActive(true);
        public void SetLineOff() => gameObject.SetActive(false);
    }
}
