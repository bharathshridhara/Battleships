using System.Linq;
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

            //Check if the new ship fouls on existing ships on the board
            int startingPoint = 0, endingPoint = 0;
            if (ship.Orientation == Orientation.Horizontal)
            {
                startingPoint = ship.BowX;
                endingPoint = ship.BowX + ship.Length - 1;
                for (int i = startingPoint; i <= endingPoint; i++)
                {
                    TryPlaceShip(i, ship.BowY, board.Cells);
                }
            }
            else
            {
                startingPoint = ship.BowY;
                endingPoint = ship.BowY - (ship.Length - 1);

                for (int i = startingPoint; i >= endingPoint; i--)
                {
                    TryPlaceShip(ship.BowX, i, board.Cells);
                }
            }
            
            return GetDto(
                await _repository.CreateShip(boardId, GetEntity(ship)));
        }

        private void TryPlaceShip(int x, int y, Occupancy[,] cells)
        {
            if (cells[x, y] == Occupancy.Occupied)
            {
                throw new InvalidShipPositionException($"Collision detected at position [{x},{y}]. Ship cannot be placed here");
            }

            cells[x, y] = Occupancy.Occupied;
        }

        private Ship GetEntity(ShipDto ship) => ShipDto.ConvertToEntity(ship);

        private ShipDto GetDto(Ship ship) => ShipDto.ConvertFromEntity(ship);

        private bool CanShipFitOnBoard(Board board, ShipDto ship)
        {
            if (ship.Orientation == Orientation.Horizontal) return ship.Length - 1 <= board.Length - ship.BowX;

            return ship.Length - 1 <= board.Breadth - ship.BowY;
        }
    }
}