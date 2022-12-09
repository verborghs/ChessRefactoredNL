using BoardSystem;
using CommandSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ChessSystem
{
    public class MoveSetCollection<TPiece>
         where TPiece : IPiece
    {
        private Dictionary<PieceType, MoveSet<TPiece>> _movesets = new Dictionary<PieceType, MoveSet<TPiece>>();

        internal MoveSetCollection(Board<TPiece> board, CommandQueue commandQueue)
        {
            _movesets.Add(PieceType.Pawn,
                new ConfigurableMoveSet<TPiece>(board, commandQueue,
                    (b, p) =>  new MoveSetHelper<TPiece>(b, p)
                                    .Forward(1)
                                    //.Forward(1, (p, b, t) => !b.TryGetPieceAt(t, out var _))
                                    //.ForwardLeft(1, (p,b,t) => b.TryGetPieceAt(t, out var _))
                                    //.ForwardRight(1, (p, b, t) => b.TryGetPieceAt(t, out var _))
                                    //.Forward(2, (p,b,t) => b.TryGetPieceAt(p, out var piece ) && piece.HasMoved)
                                    .ValidPositions()
            ));

            _movesets.Add(PieceType.Rook,
                new ConfigurableMoveSet<TPiece>(board, commandQueue,
                    (b, p) => new MoveSetHelper<TPiece>(b, p)
                                    .Forward()
                                    .Right()
                                    .Backward()
                                    .Left()
                                    .ValidPositions()
            ));

            _movesets.Add(PieceType.Bishop,
                new ConfigurableMoveSet<TPiece>(board, commandQueue,
                    (b, p) => new MoveSetHelper<TPiece>(b, p)
                                    .ForwardRight()
                                    .BackwardRight()
                                    .BackwardLeft()
                                    .ForwardLeft()
                                    .ValidPositions()
            ));

            _movesets.Add(PieceType.Queen, 
                new ConfigurableMoveSet<TPiece>(board, commandQueue,
                    (b, p) => new MoveSetHelper<TPiece>(b,p)
                                    .Forward()
                                    .ForwardRight()
                                    .Right()
                                    .BackwardRight()
                                    .Backward()
                                    .BackwardLeft()
                                    .Left()
                                    .ForwardLeft()
                                    .ValidPositions()
            ));

            _movesets.Add(PieceType.King,
                new ConfigurableMoveSet<TPiece>(board, commandQueue,
                    (b, p) => new MoveSetHelper<TPiece>(b, p)
                                    .Forward(1)
                                    .ForwardRight(1)
                                    .Right(1)
                                    .BackwardRight(1)
                                    .Backward(1)
                                    .BackwardLeft(1)
                                    .Left(1)
                                    .ForwardLeft(1)
                                    .ValidPositions()
            ));

            _movesets.Add(PieceType.Knight,
                new ConfigurableMoveSet<TPiece>(board, commandQueue,
                    (b,p) => new MoveSetHelper<TPiece>(b,p)
                                    .Collect(new Vector2Int(1, 2), 1)
                                    .Collect(new Vector2Int(-1, 2), 1)
                                    .Collect(new Vector2Int(1, -2), 1)
                                    .Collect(new Vector2Int(-1, -2), 1)
                                    .Collect(new Vector2Int(2, 1), 1)
                                    .Collect(new Vector2Int(-2, 1), 1)
                                    .Collect(new Vector2Int(2, -1), 1)
                                    .Collect(new Vector2Int(-2, -1), 1)
                                    .ValidPositions()
                ));
        }   

        public IMoveSet For(PieceType type) => _movesets[type];

        internal bool TryGetMoveSet(PieceType type, out MoveSet<TPiece> moveSet)                 
            => _movesets.TryGetValue(type, out moveSet);
    }
}
