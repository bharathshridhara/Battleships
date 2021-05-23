namespace Battleships.Domain.Entities
{
    public class Ship
    {
        public Ship()
        {
            Bow = new Coordinates(0, 0);
        }

        public int Id { get; set; }
        public Coordinates Bow { get; set; }
        public int Length { get; set; }
        public Orientation Orientation { get; set; }
    }
}