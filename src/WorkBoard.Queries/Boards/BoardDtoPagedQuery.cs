using MediatR;
using WorkBoard.Dtos;
using Qurl;
using System.Collections.Generic;

namespace WorkBoard.Queries.Boards
{
    public interface IBoardDtoPagedQueryHandler : IRequestHandler<BoardDtoPagedQuery, ResultModel<IEnumerable<BoardDto>>>
	{
	}

	public class BoardDtoPagedQuery : Query<BoardDtoFilter>, IRequest<ResultModel<IEnumerable<BoardDto>>>
	{
        public BoardDtoPagedQuery()
        {
            DefaultSort = ("Id", SortDirection.Ascending);
        }
	}
}
