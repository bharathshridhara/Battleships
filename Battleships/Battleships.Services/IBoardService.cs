using System.Threading.Tasks;
using Battleships.Services.Dto;

namespace Battleships.Services
{
    public interface IBoardService
    {
        public Task<BoardDto> Create();
        public Task<BoardDto> Get(int id);
        public Task<AttackOutcome> Attack(int boardId, AttackDto attack);
        public Task Delete(int id);
    }
}