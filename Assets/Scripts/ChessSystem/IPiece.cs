using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessSystem
{
    public interface IPiece
    {
        public PieceType Type { get;  }

        public Player Player { get; }

        //public bool HasMoved { get;  }
    }
}
