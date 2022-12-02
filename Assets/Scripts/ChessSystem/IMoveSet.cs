using BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessSystem
{
    public interface IMoveSet
    {
        List<Position> Positions(Position fromPosition);
    }
}
