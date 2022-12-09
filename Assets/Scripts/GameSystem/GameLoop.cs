using BoardSystem;
using ChessSystem;
using CommandSystem;
using GameSystem.GameStates;
using GameSystem.Helpers;
using GameSystem.Views;
using UnityEngine;


namespace GameSystem
{
    public class GameLoop : MonoBehaviour
    {
        private StateMachine _stateMachine;
        private CommandQueue _commandQueue;

        private void Start()
        {
            _commandQueue = new CommandQueue();

            _stateMachine = new StateMachine();
            _stateMachine.Register(States.Menu, new MenuState());
            _stateMachine.Register(States.Playing, new PlayState(_commandQueue));
            _stateMachine.Register(States.Replay, new ReplayState(_commandQueue));
            _stateMachine.InitialState = States.Menu;

        }
    }

}