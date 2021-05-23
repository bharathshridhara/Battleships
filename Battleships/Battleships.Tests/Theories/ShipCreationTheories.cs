using Battleships.Domain.Entities;
using Battleships.Services.Dto;
using Battleships.Test.Builders;
using Xunit;

namespace Battleships.Test.Theories
{
    public class ShipCreationTheories : TheoryData<string, ShipCreationTheoryData>
    {
        public ShipCreationTheories()
        {
            AddShipWithBowXOutsideBoard();
            AddShipWithBowYOutsideBoard();
            AddShipWithLengthOutsideBoard();
        }

        private void AddShipWithLengthOutsideBoard()
        {
            Add("Length of ship outside board limits", new ShipCreationTheoryData
            {
                ShipDto = new ShipBuilder()
                    .WithLength(20) //board dimension is 10X10
                    .Build(),
                Board = CreateBoard()
            });
        }

        private void AddShipWithBowYOutsideBoard()
        {
            Add("BowY of ship outside board limits", new ShipCreationTheoryData
            {
                ShipDto = new ShipBuilder()
                    .WithLength(2)
                    .WithBowY(13) //board dimension is 10X10
                    .Build(),
                Board = CreateBoard()
            });
        }

        private void AddShipWithBowXOutsideBoard()
        {
            Add("BowY of ship outside board limits", new ShipCreationTheoryData
            {
                ShipDto = new ShipBuilder()
                    .WithLength(2)
                    .WithBowX(2)
                    .WithBowX(13) //board dimension is 10X10
                    .Build(),
                Board = CreateBoard()
            });
        }

        private Board CreateBoard()
        {
            return new Board
            {
                Id = 1
            };
        }
    }

    public class ShipCreationTheoryData
    {
        public ShipDto ShipDto { get; set; }
        public Board Board { get; set; }
    }
}