using System;
using Mapster;
using Hco.Base.DataAccess;
using WorkBoard.Domain.Model;
using WorkBoard.Application.Dtos;

namespace WorkBoard.DataAccess.Ef.BoardDataAccess.Repository
{
    public class BoardDataAccessMapper
    {
		static BoardDataAccessMapper()
        {
            TypeAdapterConfig<Board, BoardDtoDataAccess>.NewConfig()
                .Map(dst => dst.Id, src => src.Id)
                .Map(dst => dst.Version, src => src.AggregateVersion)
                .Map(dst => dst.Guid, src => src.AggregateGuid)
                .Map(dst => dst.Title, "_title")
                .Map(dst => dst.Description, "_description")
                .Map(dst => dst.State, "_state")
                .IgnoreNonMapped(true);

			TypeAdapterConfig<BoardDtoDataAccess, Board>.NewConfig()
                .Map(dst => dst.Id, src => src.Id)
                .Map(dst => dst.AggregateVersion, src => src.Version)
                .Map(dst => dst.AggregateGuid, src => src.Guid)
                .Map("_title", dst => dst.Title)
                .Map("_description", dst => dst.Description)
                .Map("_state", dst => dst.State)
                .IgnoreNonMapped(true)
                .ConstructUsing(dst => MapperHelper.CreateInstanceWithDefaultConstructor<Board>());
        }

		public static BoardDtoDataAccess Map(WorkBoardContext context, Board board)
		{
			var boardDto = new BoardDtoDataAccess();
			Map(context, board, ref boardDto);
			return boardDto;
		}

		public static void Map(WorkBoardContext context, Board board, ref BoardDtoDataAccess boardDtoDataAccess)
        {
            boardDtoDataAccess = board.Adapt(boardDtoDataAccess);
			// TODO map missing properties
        }

		public static Board Map(WorkBoardContext context, BoardDtoDataAccess boardDtoDataAccess)
		{
			var board = MapperHelper.CreateInstanceWithDefaultConstructor<Board>();
			Map(context, boardDtoDataAccess, ref board);
			return board;
		}

		public static void Map(WorkBoardContext context, BoardDtoDataAccess boardDtoDataAccess, ref Board board)
        {
            board = boardDtoDataAccess.Adapt(board);
			// TODO map missing properties
        }
	}

}
