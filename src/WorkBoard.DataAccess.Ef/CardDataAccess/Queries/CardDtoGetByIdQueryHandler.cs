using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using WorkBoard.Application.Dtos;
using WorkBoard.Application.Queries.Cards;
using WorkBoard.Application.Queries;

namespace WorkBoard.DataAccess.Ef.CardDataAccess.Queries
{
    public class CardDtoGetByIdQueryHandler : ICardDtoGetByIdQueryHandler
    {
        private readonly WorkBoardContext _context;
		private readonly IMediator _mediator;

        public CardDtoGetByIdQueryHandler(WorkBoardContext context, IMediator mediator)
        {
			_context = context;
			_mediator = mediator;
        }

		public async Task<ResultModel<CardDto>> Handle(CardDtoGetByIdQuery request, CancellationToken cancellationToken)
        {
            IQueryable<CardDtoDataAccess> efQuery = _context.Set<CardDtoDataAccess>();
			efQuery = efQuery.AddIncludes();
			var result = new ResultModel<CardDto>
            {
                Data = (await efQuery
						.AsNoTracking()
                        .FirstOrDefaultAsync(m => request.Id == m.Id, cancellationToken))
                        .Adapt<CardDto>()
            };
			if (result.Data == null) return result;
			return result;
        }

	}
}
