using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using WorkBoard.Queries.Boards;
using WorkBoard.Dtos;
using WorkBoard.Queries;
using Qurl.Queryable;

namespace WorkBoard.DataAccess.Ef.BoardDataAccess.Queries
{
    public class BoardDtoPagedQueryHandler : IBoardDtoPagedQueryHandler
    {
        private readonly WorkBoardContext _context;

        public BoardDtoPagedQueryHandler(WorkBoardContext context)
        {
			_context = context;
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
