using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorkBoard.DataAccess.Ef.BoardDataAccess
{
    public class BoardDtoDataAccessUserDtoConfiguration : IEntityTypeConfiguration<BoardDtoDataAccessUserDto>
    {
        public void Configure(EntityTypeBuilder<BoardDtoDataAccessUserDto> builder)
        {
            builder.HasKey(m => new { m.UserId, m.BoardId });
            builder.ToTable("BoardUser", WorkBoardContext.WorkBoardSchema);
            builder.HasOne(m => m.Board).WithMany(m => m.UsersDataAccess);
            builder.HasOne(m => m.User)
                .WithMany().OnDelete(DeleteBehavior.Restrict);
        }
    }
}