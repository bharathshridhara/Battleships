using System.Collections.Generic;

namespace Battleships.Domain.Entities
{
    public class Board
    {
        public Board()
        {
            Length = 10;
            Breadth = 10;
        }

        public int Id { get; set; }
        public int Length { get; }
        public int Breadth { get; }

        public IEnumerable<Ship> Ships { get; set; } = new List<Ship>();
    }
}