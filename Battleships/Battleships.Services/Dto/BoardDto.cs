using Battleships.Domain.Entities;

namespace Battleships.Services.Dto
{
    public class BoardDto
    {
        public int Id { get; set; }
        public int Length { get; set; }
        public int Breadth { get; set; }


        public static Board ConvertToEntity(BoardDto dto)
        {
            return new Board
            {
                Id = dto.Id
            };
        }

        public static BoardDto ConvertFromEntity(Board entity)
        {
            return new BoardDto
            {
                Id = entity.Id,
                Length = entity.Length,
                Breadth = entity.Breadth
            };
        }
    }
}