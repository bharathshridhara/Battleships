using System.Threading.Tasks;
using Battleships.Data;
using Battleships.Domain.Entities;
using Battleships.Infrastructure.Exceptions;
using Battleships.Services.Dto;

namespace Battleships.Services.Impl
{
    public class ShipService : IShipService
    {
        private readonly IBattleshipsRepository _repository;

        public ShipService(IBattleshipsRepository repository)
        {
            _repository = repository;
        }

        public async Task<ShipDto> Create(int boardId, ShipDto ship)
        {
            var board = await _repository.GetBoard(boardId);
            if (board == null)
                throw new NotFoundException($"Board with ID: {boardId} not found");

            if (ship.BowX > board.Length ||
                ship.BowY > board.Breadth ||
                !CanShipFitOnBoard(board, ship))
                throw new InvalidShipPositionException(
                    "Ship cannot be positioned at this location. It is beyond board dimensions");


            // //Check if this ship would foul other ships on this board
            // var boardArray = new int[board.Length, board.Breadth];
            // for (int i = 0; i < board.Length; i++)
            // {
            //     for (int j = 0; j < board.Breadth; j++)
            //     {
            //         boardArray[i, j] = 
            //     }
            // }
            //
            //
            //     )

            return GetDto(
                await _repository.CreateShip(boardId, GetEntity(ship)));
        }

        private Ship GetEntity(ShipDto ship)
        {
            return ShipDto.ConvertToEntity(ship);
        }

        private ShipDto GetDto(Ship ship)
        {
            return ShipDto.ConvertFromEntity(ship);
        }

        private bool CanShipFitOnBoard(Board board, ShipDto ship)
        {
            if (ship.Orientation == Orientation.Horizontal) return ship.Length - 1 <= board.Length - ship.BowX;

            return ship.Length - 1 <= board.Breadth - ship.BowY;
        }
    }
}