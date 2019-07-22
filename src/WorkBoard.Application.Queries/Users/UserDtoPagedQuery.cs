using MediatR;
using WorkBoard.Application.Dtos;
using Qurl;
using System.Collections.Generic;

namespace WorkBoard.Application.Queries.Users
{
    public interface IUserDtoPagedQueryHandler : IRequestHandler<UserDtoPagedQuery, ResultModel<IEnumerable<UserDto>>>
	{
	}

	public class UserDtoPagedQuery : Query<UserDtoFilter>, IRequest<ResultModel<IEnumerable<UserDto>>>
	{
        public UserDtoPagedQuery()
        {
            DefaultSort = ("Id", SortDirection.Ascending);
        }
	}
}
