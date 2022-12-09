﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSystem
{
    public class CommandQueue
    {
        //private static CommandQueue _instance;
        //public static CommandQueue GetInstance()
        //{
        //
        //    if (_instance == null)
        //        _instance = new CommandQueue();
        //
        //    return _instance;
        //}
        //
        //private CommandQueue() { }

        private List<ICommand> _commands = new List<ICommand>();
        private int _currentCommand = -1;

        public bool IsAtStart => _currentCommand < 0;
        public bool IsAtEnd => _currentCommand >= _commands.Count - 1;

        public void Execute(ICommand command) {
            _commands.Add(command);

            while (!IsAtEnd)
                Next();

        }

        public void Previous() {
            if (IsAtStart)
                return;

            _commands[_currentCommand].Undo();
            _currentCommand--;
        }

        public void Next() {
            if (IsAtEnd)
                return;

            _currentCommand++;
            _commands[_currentCommand].Execute();
        }


    }
}
