using MediatR;
using WorkBoard.Application.Dtos;

namespace WorkBoard.Application.Queries.BoardColumns
{
    public interface IBoardColumnDtoGetByIdQueryHandler : IRequestHandler<BoardColumnDtoGetByIdQuery, ResultModel<BoardColumnDto>>
	{
	}

	public class BoardColumnDtoGetByIdQuery : IRequest<ResultModel<BoardColumnDto>>
	{
        public BoardColumnDtoGetByIdQuery()
        {
        }

        public BoardColumnDtoGetByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
        
	}
}
