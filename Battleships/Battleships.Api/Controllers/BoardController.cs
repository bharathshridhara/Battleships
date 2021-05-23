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

        [HttpPost]
        [ProducesResponseType(typeof(BoardDto), (int) HttpStatusCode.Created)]
        public async Task<IActionResult> Create()
        {
            return Ok(await _boardService.Create());
        }

        [HttpPost("{id}/attack")]
        [ProducesResponseType(typeof(AttackOutcome), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> Attack(int boardId, [FromBody] AttackDto attack)
        {
            return Ok(await _boardService.Attack(boardId, attack));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BoardDto), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _boardService.Get(id));
        }

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