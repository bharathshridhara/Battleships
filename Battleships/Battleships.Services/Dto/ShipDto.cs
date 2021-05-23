using System.ComponentModel.DataAnnotations;
using Battleships.Domain.Entities;

namespace Battleships.Services.Dto
{
    public class ShipDto
    {
        public int Id { get; set; }

        [Required] public int BowX { get; set; }

        [Required] public int BowY { get; set; }

        [Required] public int Length { get; set; }

        [Required] public Orientation Orientation { get; set; }


        public static ShipDto ConvertFromEntity(Ship ship)
        {
            return new ShipDto
            {
                Id = ship.Id,
                Orientation = ship.Orientation,
                BowX = ship.Bow.X,
                BowY = ship.Bow.Y,
                Length = ship.Length
            };
        }

        public static Ship ConvertToEntity(ShipDto ship)
        {
            return new Ship
            {
                Id = ship.Id,
                Orientation = ship.Orientation,
                Bow = new Coordinates(ship.BowX, ship.BowY),
                Length = ship.Length
            };
        }
    }
}