using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using WorkBoard.Application.Dtos;
using WorkBoard.Application.Queries.BoardColumns;
using WorkBoard.Application.Queries;

namespace WorkBoard.DataAccess.Ef.BoardColumnDataAccess.Queries
{
    public class BoardColumnDtoGetByIdQueryHandler : IBoardColumnDtoGetByIdQueryHandler
    {
        private readonly WorkBoardContext _context;
		private readonly IMediator _mediator;

        public BoardColumnDtoGetByIdQueryHandler(WorkBoardContext context, IMediator mediator)
        {
			_context = context;
			_mediator = mediator;
        }

		public async Task<ResultModel<BoardColumnDto>> Handle(BoardColumnDtoGetByIdQuery request, CancellationToken cancellationToken)
        {
            IQueryable<BoardColumnDtoDataAccess> efQuery = _context.Set<BoardColumnDtoDataAccess>();
			efQuery = efQuery.AddIncludes();
			var result = new ResultModel<BoardColumnDto>
            {
                Data = (await efQuery
						.AsNoTracking()
                        .FirstOrDefaultAsync(m => request.Id == m.Id, cancellationToken))
                        .Adapt<BoardColumnDto>()
            };
			if (result.Data == null) return result;
			return result;
        }

	}
}
