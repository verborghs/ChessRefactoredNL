using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PositionEventArgs : EventArgs
{
    public Position Position { get;  }

    public PositionEventArgs(Position position)
    {
        Position = position;
    }
}

public class BoardView : MonoBehaviour
{
    public event EventHandler<PositionEventArgs> PositionClicked;

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
