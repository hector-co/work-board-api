using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using WorkBoard.Queries.Boards;
using WorkBoard.Dtos;
using WorkBoard.Queries;
using Qurl.Queryable;

namespace WorkBoard.DataAccess.Ef.BoardDataAccess.Queries
{
    public class BoardDtoPagedQueryHandler : IBoardDtoPagedQueryHandler
    {
        private readonly WorkBoardContext _context;
		private readonly IMediator _mediator;

        public BoardDtoPagedQueryHandler(WorkBoardContext context, IMediator mediator)
        {
			_context = context;
			_mediator = mediator;
        }

        public async Task<ResultModel<IEnumerable<BoardDto>>> Handle(BoardDtoPagedQuery request, CancellationToken cancellationToken)
        {

            var result = new ResultModel<IEnumerable<BoardDto>>();

            var efQuery = _context.Set<BoardDtoDataAccess>().ApplyQuery(request, false);
            result.TotalCount = await efQuery.CountAsync();
            efQuery = efQuery.ApplySortAndPaging(request);

			efQuery = efQuery.AddIncludes();
            
            result.Data = (await efQuery
                .ToListAsync(cancellationToken))
                .Adapt<List<BoardDto>>();

            return result;
        }
	}
}
