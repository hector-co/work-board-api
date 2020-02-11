using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkBoard.Dtos;

namespace WorkBoard.DataAccess.Ef.UserDataAccess
{
    public class UserDtoConfiguration : IEntityTypeConfiguration<UserDto>
    {
        public void Configure(EntityTypeBuilder<UserDto> builder)
        {
            builder.ToTable("User", WorkBoardContext.WorkBoardSchema);
        }
    }
}