using MediatR;
using WorkBoard.Dtos;

namespace WorkBoard.Queries.Boards
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
