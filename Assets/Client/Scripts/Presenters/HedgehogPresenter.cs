using Client.Scripts.Logic;
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using Zenject;

namespace Client.Scripts.Presenters
{
    public class HedgehogPresenter : MonoBehaviour
    {
        private IStateMachineView _stateMachine;

        public void Construct(IStateMachineView stateMachine, Vector3 startPoint)
        {
            _stateMachine = stateMachine;
            transform.position = startPoint;
        }

        private void Update()
        {
            _stateMachine.ActiveState.GoToPoint(gameObject);
        }
    }
}
