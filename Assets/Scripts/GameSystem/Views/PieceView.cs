using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PieceView : MonoBehaviour
{
    public Position GridPosition => PositionHelper.GridPosition(transform.position);


    private void Awake()
    {
        var gridPositon = PositionHelper.GridPosition(transform.position);
        transform.position = PositionHelper.WorldPosition(gridPositon);
    }


    #region View Methods
    internal void MoveTo(Position toPosition)
        => transform.position = PositionHelper.WorldPosition(toPosition);


    internal void Take()
        => gameObject.SetActive(false);


    internal void Place(Position toPosition)
    {
        gameObject.SetActive(true);
        MoveTo(toPosition);
    }  
    #endregion



}

