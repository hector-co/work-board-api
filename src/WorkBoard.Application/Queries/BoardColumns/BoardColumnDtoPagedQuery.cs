using MediatR;
using WorkBoard.Application.Dtos;
using Qurl;
using System.Collections.Generic;

namespace WorkBoard.Application.Queries.BoardColumns
{
    public interface IBoardColumnDtoPagedQueryHandler : IRequestHandler<BoardColumnDtoPagedQuery, ResultModel<IEnumerable<BoardColumnDto>>>
	{
	}

	public class BoardColumnDtoPagedQuery : Query<BoardColumnDtoFilter>, IRequest<ResultModel<IEnumerable<BoardColumnDto>>>
	{
        public BoardColumnDtoPagedQuery()
        {
            DefaultSort = ("Id", SortDirection.Ascending);
        }
	}
}
