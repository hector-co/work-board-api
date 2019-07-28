using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorkBoard.DataAccess.Ef.CardDataAccess
{
    public class CardDtoDataAccessUserDtoConfiguration : IEntityTypeConfiguration<CardDtoDataAccessUserDto>
    {
        public void Configure(EntityTypeBuilder<CardDtoDataAccessUserDto> builder)
        {
            builder.HasKey(m => new { m.UserId, m.CardId });
            builder.ToTable("CardUser", WorkBoardContext.WorkBoardSchema);
            builder.HasOne(m => m.Card).WithMany(m => m.OwnersDataAccess);
            builder.HasOne(m => m.User)
                .WithMany().OnDelete(DeleteBehavior.Restrict);
        }
    }
}