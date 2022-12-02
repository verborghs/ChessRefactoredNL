using BoardSystem;
using GameSystem.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace GameSystem.Views
{
    public class PositionView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private UnityEvent OnActivate;

        [SerializeField]
        private UnityEvent OnDeactivate;

        private BoardView _boardView;
                                        
        public Position GridPosition
            => PositionHelper.GridPosition(transform.position);


        void Start()
        {
            _boardView = GetComponentInParent<BoardView>();
        }


        public void OnPointerClick(PointerEventData eventData)
            => _boardView.OnPositionViewClicked(this);

        internal void Deactivate()
            => OnDeactivate?.Invoke();

        internal void Activate()
            => OnActivate?.Invoke();
    }
}