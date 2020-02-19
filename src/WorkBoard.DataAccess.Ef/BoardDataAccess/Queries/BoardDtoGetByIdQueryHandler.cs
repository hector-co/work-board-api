using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using WorkBoard.Dtos;
using WorkBoard.Queries.Boards;
using WorkBoard.Queries;

namespace WorkBoard.DataAccess.Ef.BoardDataAccess.Queries
{
    public class BoardDtoGetByIdQueryHandler : IBoardDtoGetByIdQueryHandler
    {
        private readonly WorkBoardContext _context;

        public BoardDtoGetByIdQueryHandler(WorkBoardContext context)
        {
			_context = context;
        }

		public async Task<ResultModel<BoardDto>> Handle(BoardDtoGetByIdQuery request, CancellationToken cancellationToken)
        {
            IQueryable<BoardDtoDataAccess> efQuery = _context.Set<BoardDtoDataAccess>();
			efQuery = efQuery.AddIncludes();
			var result = new ResultModel<BoardDto>
            {
                Data = (await efQuery
						.AsNoTracking()
                        .FirstOrDefaultAsync(m => request.Id == m.Id, cancellationToken))
                        .Adapt<BoardDto>()
            };
			return result;
        }

	}
}
