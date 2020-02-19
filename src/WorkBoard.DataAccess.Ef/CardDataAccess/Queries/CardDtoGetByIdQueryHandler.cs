using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using WorkBoard.Dtos;
using WorkBoard.Queries.Cards;
using WorkBoard.Queries;

namespace WorkBoard.DataAccess.Ef.CardDataAccess.Queries
{
    public class CardDtoGetByIdQueryHandler : ICardDtoGetByIdQueryHandler
    {
        private readonly WorkBoardContext _context;

        public CardDtoGetByIdQueryHandler(WorkBoardContext context)
        {
			_context = context;
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
			return result;
        }

	}
}
