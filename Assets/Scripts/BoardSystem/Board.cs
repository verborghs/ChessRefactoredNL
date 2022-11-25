using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class PieceMovedEventArgs : EventArgs
{
    public PieceView Piece { get; }

    public Position FromPosition { get; }

    public Position ToPosition { get; }

    public PieceMovedEventArgs(PieceView piece, Position fromPosition, Position toPosition)
    {
        Piece = piece;
        FromPosition = fromPosition;
        ToPosition = toPosition;
    }
}

public class PieceTakenEventArgs : EventArgs
{
    public PieceView Piece { get; }

    public Position FromPosition { get; }

    public PieceTakenEventArgs(PieceView piece, Position fromPosition)
    {
        Piece = piece;
        FromPosition = fromPosition;
    }
}

public class PiecePlacedEventArgs : EventArgs
{
    public PieceView Piece { get; }

    public Position ToPosition { get; }

    public PiecePlacedEventArgs(PieceView piece, Position toPosition)
    {
        Piece = piece;
        ToPosition = toPosition;
    }
}

public class Board
{

    public event EventHandler<PieceMovedEventArgs> PieceMoved;
    public event EventHandler<PieceTakenEventArgs> PieceTaken;
    public event EventHandler<PiecePlacedEventArgs> PiecePlaced;

    private readonly Dictionary<Position, PieceView> _pieces = new Dictionary<Position, PieceView>();

    private readonly int _rows;
    private readonly int _columns;

    public Board(int rows, int columns)
    {
        _rows = rows;
        _columns = columns;
    }

    public bool Move(Position fromPosition, Position toPosition)
    {
        if (!IsValid(fromPosition))
            return false;

        if (!IsValid(toPosition))
            return false;

        if (_pieces.ContainsKey(toPosition))
            return false;

        if (!_pieces.TryGetValue(fromPosition, out var piece))
            return false;


        _pieces[toPosition] = piece;
        _pieces.Remove(fromPosition);

        OnPieceMoved(new PieceMovedEventArgs(piece, fromPosition, toPosition));

        return true;
    }

    public bool Place(Position toPosition, PieceView piece)
    {
        if (!IsValid(toPosition))
            return false;

        if (_pieces.ContainsKey(toPosition))
            return false;

        if (_pieces.ContainsValue(piece))
            return false;


        _pieces[toPosition] = piece;

        OnPiecePlaced(new PiecePlacedEventArgs(piece, toPosition));

        return true;
    }

    public bool Take(Position fromPosition)
    {
        if (!IsValid(fromPosition))
            return false;

        if (!_pieces.TryGetValue(fromPosition, out var piece)) 
            return false;

        _pieces.Remove(fromPosition);

        OnPieceTaken(new PieceTakenEventArgs(piece, fromPosition));

        return true;
    }

    public bool TryGetPieceAt(Position position, out PieceView piece)
        => _pieces.TryGetValue(position, out piece);

    public bool IsValid(Position position)
        => (0 <= position.X && position.X < _columns) && (0 <= position.Y && position.Y < _rows);


    #region EventTriggers
    protected virtual void OnPieceMoved(PieceMovedEventArgs eventArgs)
    {
        var handler = PieceMoved;
        handler?.Invoke(this, eventArgs);
    }

    protected virtual void OnPiecePlaced(PiecePlacedEventArgs eventArgs)
    {
        var handler = PiecePlaced;
        handler?.Invoke(this, eventArgs);
    }

    protected virtual void OnPieceTaken(PieceTakenEventArgs eventArgs)
    {
        var handler = PieceTaken;
        handler?.Invoke(this, eventArgs);
    } 
    #endregion

}

