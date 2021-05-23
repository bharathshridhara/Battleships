using System.Net;
using System.Threading.Tasks;
using Battleships.Services;
using Battleships.Services.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Battleships.Api.Controllers
{
    public class ShipController : ControllerBase
    {
        private readonly IShipService _shipService;

        public ShipController(IShipService shipService)
        {
            _shipService = shipService;
        }

        /// <summary>
        /// Create a new ship on the board specified by ID
        /// </summary>
        /// <param name="boardId">ID of the board</param>
        /// <param name="ship">The ship details to be created</param>
        /// <returns>The ship created, if successful.</returns>
        [Route("api/board/{boardId}/ship")]
        [HttpPost]
        [ProducesResponseType(typeof(ShipDto), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> Create(int boardId, [FromBody] ShipDto ship)
        {
            return Ok(await _shipService.Create(boardId, ship));
        }
    }
}