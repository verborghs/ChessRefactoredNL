using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameSystem.Views
{
    public class ReplayView : MonoBehaviour
    {
        public event EventHandler PreviousClicked;
        public event EventHandler NextClicked;


        public void Next() => OnNextClicked(EventArgs.Empty);

        public void Previous() => OnPreviousClicked(EventArgs.Empty);


        protected virtual void OnPreviousClicked(EventArgs eventArgs)
        {
            var handler = PreviousClicked;
            handler?.Invoke(this, eventArgs);
        }

        protected virtual void OnNextClicked(EventArgs eventArgs)
        {
            var handler = NextClicked;
            handler?.Invoke(this, eventArgs);
        }
    }
}
