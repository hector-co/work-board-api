using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using WorkBoard.Application.Queries.BoardColumns;

namespace WorkBoard.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/board-columns")]
    public class BoardColumnsController : Controller
    {
        private readonly IMediator _mediator;

        public BoardColumnsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet, Route("{id}", Name = "GetBoardColumnById")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            var getByIdQuery = new BoardColumnDtoGetByIdQuery(id);
            var result = await _mediator.Send(getByIdQuery, cancellationToken);
            if (result.Data == null) return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]BoardColumnDtoPagedQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }
    }
}
