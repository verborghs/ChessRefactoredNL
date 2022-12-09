using CommandSystem;
using GameSystem.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameSystem.GameStates
{
    public class ReplayState : State
    {

        private CommandQueue _commandQueue;
        private ReplayView _replayView;

        public ReplayState(CommandQueue commandQueue)
        {
            _commandQueue = commandQueue;
        }

        public override void OnEnter()
        {
            _replayView = GameObject.FindObjectOfType<ReplayView>();
            _replayView.PreviousClicked += OnPreviousClicked;
            _replayView.NextClicked += OnNextClicked;

            _commandQueue.Previous();
        }

        public override void OnExit()
        {
            _replayView.PreviousClicked -= OnPreviousClicked;
            _replayView.NextClicked -= OnNextClicked;
        }

        private void OnNextClicked(object sender, EventArgs e)
        {
            _commandQueue.Next();

            if (_commandQueue.IsAtEnd)
                StateMachine.Pop();
        }

        private void OnPreviousClicked(object sender, EventArgs e)
        {
            _commandQueue.Previous();
        }
    }
}
