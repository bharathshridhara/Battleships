using System.Net;
using System.Threading.Tasks;
using Battleships.Services;
using Battleships.Services.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Battleships.Api.Controllers
{
    //[Route("api/board/{id}/ship")]
    public class ShipController : ControllerBase
    {
        private readonly IShipService _shipService;

        public ShipController(IShipService shipService)
        {
            _shipService = shipService;
        }

        [Route("api/board/{boardId}/ship")]
        [HttpPost]
        [ProducesResponseType(typeof(ShipDto), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> Create(int boardId, [FromBody] ShipDto ship)
        {
            return Ok(await _shipService.Create(boardId, ship));
        }
    }
}