using System.Linq;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkBoard.Dtos;

namespace WorkBoard.DataAccess.Ef.CardDataAccess
{
    public class CardDtoDataAccessConfiguration : IEntityTypeConfiguration<CardDtoDataAccess>
    {
        static CardDtoDataAccessConfiguration()
        {
            TypeAdapterConfig<CardDtoDataAccess, CardDto>.ForType()
                .Map(dst => dst.Board, src => src.BoardDataAccess)
                .Map(dst => dst.Column, src => src.ColumnDataAccess)
                .Map(dst => dst.Owners, src => src.OwnersDataAccess.Select(r => r.User), src => src.OwnersDataAccess != null)
            ;
        }

        public void Configure(EntityTypeBuilder<CardDtoDataAccess> builder)
        {
            builder.ToTable("Card", WorkBoardContext.WorkBoardSchema);
            builder.Ignore(m => m.Board);
            builder.Ignore(m => m.Column);
            builder.Ignore(m => m.Owners);
            builder.HasOne(m => m.BoardDataAccess)
                .WithMany()
                .IsRequired()
                .HasForeignKey("BoardId");
            builder.HasOne(m => m.ColumnDataAccess)
                .WithMany()
                .IsRequired()
                .HasForeignKey("ColumnId");
        }
    }
}