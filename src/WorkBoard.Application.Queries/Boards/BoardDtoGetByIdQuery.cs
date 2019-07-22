using MediatR;
using WorkBoard.Application.Dtos;

namespace WorkBoard.Application.Queries.Boards
{
    public interface IBoardDtoGetByIdQueryHandler : IRequestHandler<BoardDtoGetByIdQuery, ResultModel<BoardDto>>
	{
	}

	public class BoardDtoGetByIdQuery : IRequest<ResultModel<BoardDto>>
	{
        public BoardDtoGetByIdQuery()
        {
        }

        public BoardDtoGetByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
        
	}
}
