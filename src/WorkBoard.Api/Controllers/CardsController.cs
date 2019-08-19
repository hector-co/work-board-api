using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using WorkBoard.Application.Queries.Cards;
using WorkBoard.Application.Commands.CardCommands;

namespace WorkBoard.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/cards")]
    public class CardsController : Controller
    {
        private readonly IMediator _mediator;

        public CardsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet, Route("{id}", Name = "GetCardById")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            var getByIdQuery = new CardDtoGetByIdQuery(id);
            var result = await _mediator.Send(getByIdQuery, cancellationToken);
            if (result.Data == null) return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]CardDtoPagedQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddCard([FromBody]AddCardCommand command, CancellationToken cancellationToken)
        {
            var cardId = await _mediator.Send(command);
            return await Get(cardId, cancellationToken);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditCard(int id, [FromBody]EditCardCommand command, CancellationToken cancellationToken)
        {
            command.CardId = id;
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpPost("{id}/move")]
        public async Task<IActionResult> MoveCard(int id, [FromBody]MoveCardCommand command, CancellationToken cancellationToken)
        {
            command.CardId = id;
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpPost("{id}/points")]
        public async Task<IActionResult> UpdateCardPoints(int id, [FromBody]UpdateCardPointsCommand command, CancellationToken cancellationToken)
        {
            command.CardId = id;
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }
    }
}
