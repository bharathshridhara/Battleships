using System.Threading.Tasks;
using Battleships.Domain.Entities;

namespace Battleships.Data
{
    public interface IBattleshipsRepository
    {
        public Task<Board> CreateBoard(Board board);
        public Task<Board> GetBoard(int id);

        public Task<Ship> CreateShip(int boardId, Ship ship);
        public Task DeleteBoard(int id);
    }
}