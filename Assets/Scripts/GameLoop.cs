using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class GameLoop : MonoBehaviour
{

    private void Start()
    {
        var boardView = FindObjectOfType<BoardView>();
        boardView.PositionClicked += PositionClicked; 
    }

    private void PositionClicked(object sender, PositionEventArgs eventArgs)
    {
        Debug.Log($"{eventArgs.Position}");
    }
}

