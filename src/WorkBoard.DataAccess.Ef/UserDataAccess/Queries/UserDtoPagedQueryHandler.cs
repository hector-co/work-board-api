using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using WorkBoard.Queries.Users;
using WorkBoard.Dtos;
using WorkBoard.Queries;
using Qurl.Queryable;

namespace WorkBoard.DataAccess.Ef.UserDataAccess.Queries
{
    public class UserDtoPagedQueryHandler : IUserDtoPagedQueryHandler
    {
        private readonly WorkBoardContext _context;
		private readonly IMediator _mediator;

        public UserDtoPagedQueryHandler(WorkBoardContext context, IMediator mediator)
        {
			_context = context;
			_mediator = mediator;
        }

        public async Task<ResultModel<IEnumerable<UserDto>>> Handle(UserDtoPagedQuery request, CancellationToken cancellationToken)
        {

            var result = new ResultModel<IEnumerable<UserDto>>();

            var efQuery = _context.Set<UserDto>().ApplyQuery(request, false);
            result.TotalCount = await efQuery.CountAsync();
            efQuery = efQuery.ApplySortAndPaging(request);

            result.Data = (await efQuery
                .ToListAsync(cancellationToken))
                .Adapt<List<UserDto>>();

            return result;
        }
	}
}
