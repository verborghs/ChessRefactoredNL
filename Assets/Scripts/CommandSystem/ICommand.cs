using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandSystem
{
    public interface ICommand
    {
        void Execute();

        void Undo();
    }
}