using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Battleships.Domain.Entities;

namespace Battleships.Data.Impl
{
    public class BattleshipRepository : IBattleshipsRepository
    {
        private readonly List<Board> _boards;

        public BattleshipRepository()
        {
            _boards = new List<Board>();
        }

        public Task<Board> CreateBoard(Board board)
        {
            board.Id = _boards.LastOrDefault() == null ? 1 : _boards.Last().Id + 1;
            _boards.Add(board);
            return Task.FromResult(board);
        }

        public async Task<Ship> CreateShip(int boardId, Ship ship)
        {
            var board = await GetBoard(boardId);
            ship.Id = board.Ships.Count() + 1;
            board.Ships = board.Ships.Append(ship);
            return ship;
        }

        public Task<Board> GetBoard(int id)
        {
            return Task.FromResult(_boards.FirstOrDefault(b => b.Id.Equals(id)));
        }

        public async Task DeleteBoard(int id)
        {
            var b = await GetBoard(id);

            _boards.Remove(b);
        }
    }
}