using System.Threading.Tasks;
using Battleships.Services.Dto;

namespace Battleships.Services
{
    public interface IShipService
    {
        public Task<ShipDto> Create(int boardId, ShipDto ship);
    }
}