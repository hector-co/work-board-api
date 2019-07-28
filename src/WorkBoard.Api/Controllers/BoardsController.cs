using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using WorkBoard.Application.Queries.Boards;

namespace WorkBoard.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/boards")]
    public class BoardsController : Controller
    {
        private readonly IMediator _mediator;

        public BoardsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet, Route("{id}", Name = "GetBoardById")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            var getByIdQuery = new BoardDtoGetByIdQuery(id);
            var result = await _mediator.Send(getByIdQuery, cancellationToken);
            if (result.Data == null) return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]BoardDtoPagedQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }
    }
}
