using BoardSystem;
using GameSystem.Helpers;
using UnityEngine;

namespace GameSystem.Views
{
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

}