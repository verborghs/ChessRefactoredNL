using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PositionView : MonoBehaviour, IPointerClickHandler
{
    public event EventHandler Clicked;

    public Position GridPosition 
        => PositionHelper.GridPosition(transform.position);
    
    public void OnPointerClick(PointerEventData eventData)
        => OnClicked(EventArgs.Empty);


    protected virtual void OnClicked(EventArgs eventArgs)
    {
        var handler = Clicked;
        handler?.Invoke(this, eventArgs);
    }
}
