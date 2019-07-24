using System;
using Mapster;
using Hco.Base.DataAccess;
using WorkBoard.Domain.Model;
using WorkBoard.Application.Dtos;

namespace WorkBoard.DataAccess.Ef.BoardDataAccess.Repository
{
    public class BoardColumnDataAccessMapper
    {
		static BoardColumnDataAccessMapper()
        {
            TypeAdapterConfig<BoardColumn, BoardColumnDto>.NewConfig()
                .Map(dst => dst.Id, src => src.Id)
                .Map(dst => dst.Title, "_title")
                .Map(dst => dst.Order, "_order")
                .Map(dst => dst.Description, "_description")
                .Map(dst => dst.Active, "_active")
                .IgnoreNonMapped(true);

			TypeAdapterConfig<BoardColumnDto, BoardColumn>.NewConfig()
                .Map(dst => dst.Id, src => src.Id)
                .Map("_title", dst => dst.Title)
                .Map("_order", dst => dst.Order)
                .Map("_description", dst => dst.Description)
                .Map("_active", dst => dst.Active)
                .IgnoreNonMapped(true)
                .ConstructUsing(dst => MapperHelper.CreateInstanceWithDefaultConstructor<BoardColumn>());
        }

		public static BoardColumnDto Map(WorkBoardContext context, BoardColumn boardColumn)
		{
			var boardColumnDto = new BoardColumnDto();
			Map(context, boardColumn, ref boardColumnDto);
			return boardColumnDto;
		}

		public static void Map(WorkBoardContext context, BoardColumn boardColumn, ref BoardColumnDto boardColumnDto)
        {
            boardColumnDto = boardColumn.Adapt(boardColumnDto);
			// TODO map missing properties
        }

		public static BoardColumn Map(WorkBoardContext context, BoardColumnDto boardColumnDto)
		{
			var boardColumn = MapperHelper.CreateInstanceWithDefaultConstructor<BoardColumn>();
			Map(context, boardColumnDto, ref boardColumn);
			return boardColumn;
		}

		public static void Map(WorkBoardContext context, BoardColumnDto boardColumnDto, ref BoardColumn boardColumn)
        {
            boardColumn = boardColumnDto.Adapt(boardColumn);
			// TODO map missing properties
        }
	}

}
