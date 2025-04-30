using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Client.Scripts.Logic
{
    public class HedgehogStateMachine : IStateMachineView, IStateMachineState
    {
        private Dictionary<int, IState> _states;
        public IState ActiveState { get; private set; }

        public void Construct(Dictionary<int, IState> states)
        {
            _states = states;
            ActiveState = _states.First().Value;
        }

        public void EnterNextState()
        {
            if (_states[GetNextStateID()] != null)
                ActiveState = _states[GetNextStateID()];
            else
                Debug.LogError("There is no state with id:" + GetNextStateID());
        }

        private int GetNextStateID()
        {
            foreach (var pair in _states)
            {
                if (pair.Value.Equals(ActiveState) && _states.ContainsKey(pair.Key + 1))
                    return pair.Key + 1;
            }
            return 0;
        }
    }
}
