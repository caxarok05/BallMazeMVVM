using UnityEngine;

namespace Client.Scripts.Logic
{
    public class HedgehogPoint : IState
    {
        private Vector3 _point;
        private float _speed;
        private IStateMachineState _stateMachine;

        public HedgehogPoint(Vector3 point, float speed, IStateMachineState stateMachine)
        {
            _point = point;
            _speed = speed;
            _stateMachine = stateMachine;
        }

        public void GoToPoint(GameObject hedgeHog)
        {
            Vector3 direction = _point - hedgeHog.transform.position;
            float distance = direction.magnitude;
            direction.Normalize();

            if (distance > 0.02f)
            {
                hedgeHog.transform.Translate(direction * _speed * Time.deltaTime, Space.World);
                hedgeHog.transform.rotation = Quaternion.LookRotation(-direction);

            }
            else
                _stateMachine.EnterNextState();
        }
    }
}
