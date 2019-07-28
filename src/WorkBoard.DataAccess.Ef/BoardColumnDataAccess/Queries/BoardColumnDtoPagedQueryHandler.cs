using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using WorkBoard.Application.Queries.BoardColumns;
using WorkBoard.Application.Dtos;
using WorkBoard.Application.Queries;
using Qurl.Queryable;

namespace WorkBoard.DataAccess.Ef.BoardColumnDataAccess.Queries
{
    public class BoardColumnDtoPagedQueryHandler : IBoardColumnDtoPagedQueryHandler
    {
        private readonly WorkBoardContext _context;
		private readonly IMediator _mediator;

        public BoardColumnDtoPagedQueryHandler(WorkBoardContext context, IMediator mediator)
        {
			_context = context;
			_mediator = mediator;
        }

        public async Task<ResultModel<IEnumerable<BoardColumnDto>>> Handle(BoardColumnDtoPagedQuery request, CancellationToken cancellationToken)
        {
            request.SetPropertyNameMapping("BoardId", "BoardDataAccess.Id");

            var result = new ResultModel<IEnumerable<BoardColumnDto>>();

            var efQuery = _context.Set<BoardColumnDtoDataAccess>().ApplyQuery(request, false);
            result.TotalCount = await efQuery.CountAsync();
            efQuery = efQuery.ApplySortAndPaging(request);

			efQuery = efQuery.AddIncludes();

            result.Data = (await efQuery
                .ToListAsync(cancellationToken))
                .Adapt<List<BoardColumnDto>>();

            return result;
        }
	}
}
