using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using WorkBoard.Dtos;
using WorkBoard.Queries.Users;
using WorkBoard.Queries;

namespace WorkBoard.DataAccess.Ef.UserDataAccess.Queries
{
    public class UserDtoGetByIdQueryHandler : IUserDtoGetByIdQueryHandler
    {
        private readonly WorkBoardContext _context;
		private readonly IMediator _mediator;

        public UserDtoGetByIdQueryHandler(WorkBoardContext context, IMediator mediator)
        {
			_context = context;
			_mediator = mediator;
        }

		public async Task<ResultModel<UserDto>> Handle(UserDtoGetByIdQuery request, CancellationToken cancellationToken)
        {
            IQueryable<UserDto> efQuery = _context.Set<UserDto>();
			var result = new ResultModel<UserDto>
            {
                Data = (await efQuery
						.AsNoTracking()
                        .FirstOrDefaultAsync(m => request.Id == m.Id, cancellationToken))
            };
			return result;
        }

	}
}
