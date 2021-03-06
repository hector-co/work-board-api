using System.Linq;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkBoard.Dtos;

namespace WorkBoard.DataAccess.Ef.BoardDataAccess
{
    public class BoardDtoDataAccessConfiguration : IEntityTypeConfiguration<BoardDtoDataAccess>
    {
        static BoardDtoDataAccessConfiguration()
        {
            TypeAdapterConfig<BoardDtoDataAccess, BoardDto>.ForType()
                .Map(dst => dst.Users, src => src.UsersDataAccess.Select(r => r.User), src => src.UsersDataAccess != null)
            ;
        }

        public void Configure(EntityTypeBuilder<BoardDtoDataAccess> builder)
        {
            builder.ToTable("Board", WorkBoardContext.WorkBoardSchema);
            builder.Ignore(m => m.Users);
        }
    }
}