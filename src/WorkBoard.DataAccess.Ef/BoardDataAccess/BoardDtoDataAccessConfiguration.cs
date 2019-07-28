using System.Linq;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkBoard.Application.Dtos;

namespace WorkBoard.DataAccess.Ef.BoardDataAccess
{
    public class BoardDtoDataAccessConfiguration : IEntityTypeConfiguration<BoardDtoDataAccess>
    {
        static BoardDtoDataAccessConfiguration()
		{
            TypeAdapterConfig<BoardDtoDataAccess, BoardDto>.ForType()
                .Map(dst => dst.Users, src => src.UsersDataAccess.Select(r => r.User))
            ;
		}

        public void Configure(EntityTypeBuilder<BoardDtoDataAccess> builder)
        {
            builder.ToTable("Board", WorkBoardContext.WorkBoardSchema);
            builder.Ignore(m => m.Users);
        }
    }
}