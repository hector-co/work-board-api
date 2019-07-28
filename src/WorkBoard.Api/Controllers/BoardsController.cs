using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using WorkBoard.Application.Queries.Boards;
using WorkBoard.Application.Commands.BoardCommands;
using WorkBoard.Application.Queries.BoardColumns;
using Qurl;
using WorkBoard.Application.Commands.BoardColumnCommands;

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

        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegisterBoardCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]UpdateBoardCommand command)
        {
            command.BoardId = id;
            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet("{id}/columns")]
        public async Task<IActionResult> GetColumns(int id, [FromQuery]BoardColumnDtoPagedQuery query, CancellationToken cancellationToken)
        {
            query.Filter.BoardId = new EqualsFilterProperty<int>
            {
                Value = id
            };
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpPost("{id}/columns")]
        public async Task<IActionResult> AddColumn(int id, [FromBody]AddColumnCommand command)
        {
            command.BoardId = id;
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut("{id}/columns/{columnId}")]
        public async Task<IActionResult> EditColumn(int id, int columnId, [FromBody]EditColumnCommand command)
        {
            command.BoardId = id;
            command.ColumnId = columnId;
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{id}/columns/{columnId}")]
        public async Task<IActionResult> DeleteColumn(int id, int columnId)
        {
            var command = new DeleteColumnCommand
            {
                BoardId = id,
                ColumnId = columnId
            };
            await _mediator.Send(command);
            return Ok();
        }
    }
}
