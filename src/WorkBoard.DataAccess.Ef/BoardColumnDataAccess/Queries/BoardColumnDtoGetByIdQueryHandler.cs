using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using WorkBoard.Dtos;
using WorkBoard.Queries.BoardColumns;
using WorkBoard.Queries;

namespace WorkBoard.DataAccess.Ef.BoardColumnDataAccess.Queries
{
    public class BoardColumnDtoGetByIdQueryHandler : IBoardColumnDtoGetByIdQueryHandler
    {
        private readonly WorkBoardContext _context;

        public BoardColumnDtoGetByIdQueryHandler(WorkBoardContext context)
        {
			_context = context;
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
