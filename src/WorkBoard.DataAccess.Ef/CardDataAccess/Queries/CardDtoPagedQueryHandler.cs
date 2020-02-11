using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using WorkBoard.Queries.Cards;
using WorkBoard.Dtos;
using WorkBoard.Queries;
using Qurl.Queryable;

namespace WorkBoard.DataAccess.Ef.CardDataAccess.Queries
{
    public class CardDtoPagedQueryHandler : ICardDtoPagedQueryHandler
    {
        private readonly WorkBoardContext _context;
		private readonly IMediator _mediator;

        public CardDtoPagedQueryHandler(WorkBoardContext context, IMediator mediator)
        {
			_context = context;
			_mediator = mediator;
        }

        public async Task<ResultModel<IEnumerable<CardDto>>> Handle(CardDtoPagedQuery request, CancellationToken cancellationToken)
        {
            request.SetPropertyNameMapping("BoardId", "BoardDataAccess.Id");
            request.SetPropertyNameMapping("ColumnId", "BoardColumnDataAccess.Id");

            var result = new ResultModel<IEnumerable<CardDto>>();

            var efQuery = _context.Set<CardDtoDataAccess>().ApplyQuery(request, false);
            result.TotalCount = await efQuery.CountAsync();
            efQuery = efQuery.ApplySortAndPaging(request);

			efQuery = efQuery.AddIncludes();
            
            result.Data = (await efQuery
                .ToListAsync(cancellationToken))
                .Adapt<List<CardDto>>();

            return result;
        }
	}
}
