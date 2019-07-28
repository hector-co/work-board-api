using MediatR;
using WorkBoard.Application.Dtos;
using Qurl;
using System.Collections.Generic;

namespace WorkBoard.Application.Queries.Cards
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
