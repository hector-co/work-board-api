using MediatR;
using WorkBoard.Dtos;
using Qurl;
using System.Collections.Generic;

namespace WorkBoard.Queries.Cards
{
    public interface ICardDtoPagedQueryHandler : IRequestHandler<CardDtoPagedQuery, ResultModel<IEnumerable<CardDto>>>
	{
	}

	public class CardDtoPagedQuery : Query<CardDtoFilter>, IRequest<ResultModel<IEnumerable<CardDto>>>
	{
        public CardDtoPagedQuery()
        {
            DefaultSort = ("Id", SortDirection.Ascending);
        }
	}
}
