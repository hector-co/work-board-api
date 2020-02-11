using System;
using Mapster;
using Hco.Base.DataAccess;
using WorkBoard.Domain.Model;
using WorkBoard.Dtos;

namespace WorkBoard.DataAccess.Ef.BoardColumnDataAccess.Repository
{
    public class BoardColumnDataAccessMapper
    {
		static BoardColumnDataAccessMapper()
        {
            TypeAdapterConfig<BoardColumn, BoardColumnDtoDataAccess>.NewConfig()
                .Map(dst => dst.Id, src => src.Id)
                .Map(dst => dst.Version, src => src.AggregateVersion)
                .Map(dst => dst.Guid, src => src.AggregateGuid)
                .Map(dst => dst.Title, "_title")
                .Map(dst => dst.Order, "_order")
                .Map(dst => dst.Description, "_description")
                .Map(dst => dst.Active, "_active")
                .IgnoreNonMapped(true);

			TypeAdapterConfig<BoardColumnDtoDataAccess, BoardColumn>.NewConfig()
                .Map(dst => dst.Id, src => src.Id)
                .Map(dst => dst.AggregateVersion, src => src.Version)
                .Map(dst => dst.AggregateGuid, src => src.Guid)
                .Map("_title", dst => dst.Title)
                .Map("_order", dst => dst.Order)
                .Map("_description", dst => dst.Description)
                .Map("_active", dst => dst.Active)
                .IgnoreNonMapped(true)
                .ConstructUsing(dst => MapperHelper.CreateInstanceWithDefaultConstructor<BoardColumn>());
        }

		public static BoardColumnDtoDataAccess Map(WorkBoardContext context, BoardColumn boardColumn)
		{
			var boardColumnDto = new BoardColumnDtoDataAccess();
			Map(context, boardColumn, ref boardColumnDto);
			return boardColumnDto;
		}

		public static void Map(WorkBoardContext context, BoardColumn boardColumn, ref BoardColumnDtoDataAccess boardColumnDtoDataAccess)
        {
            boardColumnDtoDataAccess = boardColumn.Adapt(boardColumnDtoDataAccess);
			// TODO map missing properties
        }

		public static BoardColumn Map(WorkBoardContext context, BoardColumnDtoDataAccess boardColumnDtoDataAccess)
		{
			var boardColumn = MapperHelper.CreateInstanceWithDefaultConstructor<BoardColumn>();
			Map(context, boardColumnDtoDataAccess, ref boardColumn);
			return boardColumn;
		}

		public static void Map(WorkBoardContext context, BoardColumnDtoDataAccess boardColumnDtoDataAccess, ref BoardColumn boardColumn)
        {
            boardColumn = boardColumnDtoDataAccess.Adapt(boardColumn);
			// TODO map missing properties
        }
	}

}
