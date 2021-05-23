using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Battleships.Data;
using Battleships.Domain.Entities;

namespace Battleships.Test.TestDoubles
{
    public class BattleshipRepositorySpy : IBattleshipsRepository
    {
        public List<Board> Boards { get; set; }

        public Task<Board> CreateBoard(Board board)
        {
            Boards.Add(board);
            return Task.FromResult(board);
        }

        public Task<Board> GetBoard(int id)
        {
            return Task.FromResult(Boards.FirstOrDefault(b => b.Id == id));
        }

        public Task<Ship> CreateShip(int boardId, Ship ship)
        {
            var board = GetBoard(boardId).Result;
            board.Ships = board.Ships.Append(ship);

            return Task.FromResult(ship);
        }

        public Task DeleteBoard(int id)
        {
            throw new NotImplementedException();
        }
    }
}