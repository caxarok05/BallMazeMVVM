using Client.Scripts.Logic;
using Client.Scripts.Services;
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using Zenject;

namespace Client.Scripts.Presenters
{
    public class HedgehogPresenter : MonoBehaviour
    {
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
                Vector3 direction = collision.transform.position - transform.position;
                if (Vector3.Dot(transform.forward, direction) > 0.8 || Vector3.Dot(transform.forward, direction) < -0.8)
                {
                    Debug.Log("front collision");
                    gameObject.SetActive(false);
                }
                else
                {
                    _healthService.SpendHealth(1);
                    Debug.Log("SideCollision");
                }
            }
        }
    }
}
