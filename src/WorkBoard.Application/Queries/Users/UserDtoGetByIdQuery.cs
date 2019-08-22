using MediatR;
using WorkBoard.Application.Dtos;

namespace WorkBoard.Application.Queries.Users
{
    public interface IUserDtoGetByIdQueryHandler : IRequestHandler<UserDtoGetByIdQuery, ResultModel<UserDto>>
	{
	}

	public class UserDtoGetByIdQuery : IRequest<ResultModel<UserDto>>
	{
        public UserDtoGetByIdQuery()
        {
        }

        public UserDtoGetByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
        
	}
}
