using System.Collections;
using System.Collections.Generic;
//using UnityEngine;

namespace StatePattern
{
    public class StateMachine
    {
        private IState _currentState;
        public StateMachine(IState startingState)
        {
            _currentState = startingState;
            _currentState.OnEnter();
        }

        public void Update()
        {
            _currentState.Update();
        }

        public void MoveToState(IState nextState)
        {
            _currentState.OnExit();
            _currentState = nextState;
            _currentState.OnEnter();
        }
    }
}
