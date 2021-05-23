using System;
using System.Collections.Generic;

namespace Battleships.Domain.Entities
{
    public class Board
    {
        public Board()
        {
            Length = 10;
            Breadth = 10;
            Cells = new Occupancy[Length, Breadth];
        }

        public int Id { get; set; }
        public int Length { get; }
        public int Breadth { get; }
        
        public Occupancy[,] Cells { get; set; }

        public IEnumerable<Ship> Ships { get; set; } = new List<Ship>();
    }
}