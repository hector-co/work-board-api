using MediatR;
using WorkBoard.Dtos;

namespace WorkBoard.Queries.Cards
{
    public interface ICardDtoGetByIdQueryHandler : IRequestHandler<CardDtoGetByIdQuery, ResultModel<CardDto>>
	{
	}

	public class CardDtoGetByIdQuery : IRequest<ResultModel<CardDto>>
	{
        public CardDtoGetByIdQuery()
        {
        }

        public CardDtoGetByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
        
	}
}
