using System;
using Mapster;
using Hco.Base.DataAccess;
using WorkBoard.Domain.Model;
using WorkBoard.Dtos;

namespace WorkBoard.DataAccess.Ef.CardDataAccess.Repository
{
    public class CardDataAccessMapper
    {
		static CardDataAccessMapper()
        {
            TypeAdapterConfig<Card, CardDtoDataAccess>.NewConfig()
                .Map(dst => dst.Id, src => src.Id)
                .Map(dst => dst.Version, src => src.AggregateVersion)
                .Map(dst => dst.Guid, src => src.AggregateGuid)
                .Map(dst => dst.Title, "_title")
                .Map(dst => dst.Description, "_description")
                .Map(dst => dst.Color, "_color")
                .Map(dst => dst.Priority, "_priority")
                .Map(dst => dst.EstimatedPoints, "_estimatedPoints")
                .Map(dst => dst.ConsumedPoints, "_consumedPoints")
                .Map(dst => dst.Done, "_done")
                .Map(dst => dst.Order, "_order")
                .IgnoreNonMapped(true);

			TypeAdapterConfig<CardDtoDataAccess, Card>.NewConfig()
                .Map(dst => dst.Id, src => src.Id)
                .Map(dst => dst.AggregateVersion, src => src.Version)
                .Map(dst => dst.AggregateGuid, src => src.Guid)
                .Map("_title", dst => dst.Title)
                .Map("_description", dst => dst.Description)
                .Map("_color", dst => dst.Color)
                .Map("_priority", dst => dst.Priority)
                .Map("_estimatedPoints", dst => dst.EstimatedPoints)
                .Map("_consumedPoints", dst => dst.ConsumedPoints)
                .Map("_done", dst => dst.Done)
                .Map("_order", dst => dst.Order)
                .IgnoreNonMapped(true)
                .ConstructUsing(dst => MapperHelper.CreateInstanceWithDefaultConstructor<Card>());
        }

		public static CardDtoDataAccess Map(WorkBoardContext context, Card card)
		{
			var cardDto = new CardDtoDataAccess();
			Map(context, card, ref cardDto);
			return cardDto;
		}

		public static void Map(WorkBoardContext context, Card card, ref CardDtoDataAccess cardDtoDataAccess)
        {
            cardDtoDataAccess = card.Adapt(cardDtoDataAccess);
			// TODO map missing properties
        }

		public static Card Map(WorkBoardContext context, CardDtoDataAccess cardDtoDataAccess)
		{
			var card = MapperHelper.CreateInstanceWithDefaultConstructor<Card>();
			Map(context, cardDtoDataAccess, ref card);
			return card;
		}

		public static void Map(WorkBoardContext context, CardDtoDataAccess cardDtoDataAccess, ref Card card)
        {
            card = cardDtoDataAccess.Adapt(card);
			// TODO map missing properties
        }
	}

}
