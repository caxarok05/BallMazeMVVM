using Client.Scripts.Logic;
using Client.Scripts.Services;
using UnityEngine;
using Zenject;

namespace Client.Scripts.Presenters
{
    public class HedgehogPresenter : MonoBehaviour
    {
        private const float AngleLimit = 15;
        private const int PlayerLayer = 7;
        
        private IStateMachineView _stateMachine;
        private IHealthService _healthService;

        [Inject]
        public void Construct(IHealthService healthService)
        {
            _healthService = healthService;
        }

        public void Init(IStateMachineView stateMachine, Vector3 startPoint)
        {
            _stateMachine = stateMachine;
            transform.position = startPoint;
        }

        private void Update()
        {
            _stateMachine.ActiveState.GoToPoint(gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == PlayerLayer)
            {
                if (CustomVelocity.CalculateEntryAngle(transform, collision.transform.position, AngleLimit))
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    _healthService.SpendHealth(1);
                }
            }
        }
    }
}
