using BoardSystem;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace GameSystem.Views
{
    public class PositionEventArgs : EventArgs
    {
        public Position Position { get; }


        public PositionEventArgs(Position position)
        {
            Position = position;
        }
    }

    public class BoardView : MonoBehaviour
    {
        private List<Position> _activatedPositions = new List<Position>(0);

        private Dictionary<Position, PositionView> _positionViewsCached = new Dictionary<Position, PositionView>();

        public List<Position> ActivatedPositions { 
        set
            {

                foreach (var position in _activatedPositions)
                    _positionViewsCached[position].Deactivate();

                if (value == null)
                    _activatedPositions = new List<Position>(0);
                else
                    _activatedPositions = value;


                foreach(var position in _activatedPositions)
                            _positionViewsCached[position].Activate();
            }
        }

        public event EventHandler<PositionEventArgs> PositionClicked;


        private void Start()
        {
            foreach (var positionView in GetComponentsInChildren<PositionView>())
                _positionViewsCached.Add(positionView.GridPosition, positionView);
        }


        internal void OnPositionViewClicked(PositionView positionView)
        {
            OnPositionClicked(new PositionEventArgs(positionView.GridPosition));
        }

        protected virtual void OnPositionClicked(PositionEventArgs eventArgs)
        {
            var handler = PositionClicked;
            handler?.Invoke(this, eventArgs);
        }
    }
}