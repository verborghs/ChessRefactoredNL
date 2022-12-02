using System.Collections.Generic;

namespace GameSystem.GameStates
{

    public enum States
    {
        Menu, Playing
    }

    public class StateMachine
    {
        private Dictionary<States, State> _states = new Dictionary<States, State>();
        private States _currentStateName;

        public State CurrentState => _states[_currentStateName];

        public void Register(States stateName, State state)
        {
            state.StateMachine = this;

            _states.Add(stateName, state);
        }

        public States InitialState
        {
            set
            {
                _currentStateName = value;
                CurrentState.OnEnter();
            }
        }

        public void MoveTo(States stateName)
        {
            CurrentState.OnExit();
            _currentStateName = stateName;
            CurrentState.OnEnter();
        }

    }
}