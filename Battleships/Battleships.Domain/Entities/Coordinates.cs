namespace Battleships.Domain.Entities
{
    public class Coordinates
    {
        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }
    }
}