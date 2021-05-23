using System.Linq;
using System.Threading.Tasks;
using Battleships.Data;
using Battleships.Domain.Entities;
using Battleships.Infrastructure.Exceptions;
using Battleships.Services.Dto;

namespace Battleships.Services.Impl
{
    public class BoardService : IBoardService
    {
        private readonly IBattleshipsRepository _repository;

        public BoardService(IBattleshipsRepository repository)
        {
            _repository = repository;
        }

        public async Task<BoardDto> Create()
        {
            var boardEntity = await _repository.CreateBoard(new Board());
            return GetDto(boardEntity);
        }

        public async Task<AttackOutcome> Attack(int boardId, AttackDto attack)
        {
            var board = await GetBoard(boardId);

            if (attack.AttackX > board.Length || attack.AttackY > board.Breadth)
                throw new InvalidAttackException(
                    $"Attack outside the board dimension. Board is {board.Length} X {board.Breadth}");

            //Check horizontally

            var hit = board.Ships.Any(s =>
                s.Orientation.Equals(Orientation.Horizontal) &&
                s.Bow.Y == attack.AttackY &&
                s.Bow.X <= attack.AttackX && s.Bow.X + (s.Length - 1) >= attack.AttackX
                ||
                s.Orientation.Equals(Orientation.Vertical) &&
                s.Bow.X == attack.AttackX &&
                s.Bow.Y >= attack.AttackY && s.Bow.Y - (s.Length - 1) <= attack.AttackY);
            
            return new AttackOutcome
            {
                Outcome = hit ? "Hit" : "Miss"
            };
        }

        public async Task<BoardDto> Get(int id)
        {
            return GetDto(await GetBoard(id));
        }

        public async Task Delete(int id)
        {
            await _repository.DeleteBoard(id);
        }
        
        private BoardDto GetDto(Board board)
        {
            return BoardDto.ConvertFromEntity(board);
        }

        private async Task<Board> GetBoard(int id)
        {
            var board = await _repository.GetBoard(id);
            if (board == null) throw new NotFoundException($"Board with ID: {id} not found");

            return board;
        }
    }
}