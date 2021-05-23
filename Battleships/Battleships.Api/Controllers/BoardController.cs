using System.Net;
using System.Threading.Tasks;
using Battleships.Services;
using Battleships.Services.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Battleships.Api.Controllers
{
    [ApiController]
    [Route("api/board")]
    public class BoardController : ControllerBase
    {
        private readonly IBoardService _boardService;

        public BoardController(IBoardService boardService)
        {
            _boardService = boardService;
        }

        /// <summary>
        /// Creates a new board
        /// </summary>
        /// <returns>The created board</returns>
        [HttpPost]
        [ProducesResponseType(typeof(BoardDto), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> Create()
        {
            return Ok(await _boardService.Create());
        }

        /// <summary>
        /// Send an attack on the board
        /// </summary>
        /// <param name="boardId">The board to launch the attack on</param>
        /// <param name="attack">X and Y co-ordinates for the attack</param>
        /// <returns></returns>
        [HttpPost("{boardId}/attack")]
        [ProducesResponseType(typeof(AttackOutcome), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> Attack(int boardId, [FromBody] AttackDto attack)
        {
            return Ok(await _boardService.Attack(boardId, attack));
        }

        /// <summary>
        /// Returns the board queried by ID
        /// </summary>
        /// <param name="id">ID of the board</param>
        /// <returns>The Board queried in the request</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BoardDto), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _boardService.Get(id));
        }

        /// <summary>
        /// Delete the board by ID
        /// </summary>
        /// <param name="id">ID of the board</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await _boardService.Delete(id);
            return Ok();
        }
    }
}