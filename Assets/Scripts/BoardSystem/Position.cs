namespace BoardSystem
{
    public struct Position
    {
        private readonly int _x;
        private readonly int _y;

        public int X => _x;
        public int Y => _y;

        public Position(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public override string ToString()
        {
            return $"Position({X}, {Y})";
        }
    }

}