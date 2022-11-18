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

    private void Start()
    {
        var positionViews = GetComponentsInChildren<PositionView>();
        foreach (var positionView in positionViews)
            positionView.Clicked += OnPositionViewClicked;
    }
    
    private void OnPositionViewClicked(object sender, EventArgs e)
    {
        if (sender is PositionView positionView)
            OnPositionClicked(new PositionEventArgs(positionView.GridPosition));
    }


    public void ClickedAt(Position position)
    {
        OnPositionClicked(new PositionEventArgs(position));
    }


    protected virtual void OnPositionClicked(PositionEventArgs eventArgs)
    {
        var handler = PositionClicked;
        handler?.Invoke(this, eventArgs);
    } 
}
