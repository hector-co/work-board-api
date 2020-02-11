using System.Linq;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkBoard.Dtos;

namespace WorkBoard.DataAccess.Ef.BoardColumnDataAccess
{
    public class BoardColumnDtoDataAccessConfiguration : IEntityTypeConfiguration<BoardColumnDtoDataAccess>
    {
        static BoardColumnDtoDataAccessConfiguration()
        {
            TypeAdapterConfig<BoardColumnDtoDataAccess, BoardColumnDto>.ForType()
                .Map(dst => dst.Board, src => src.BoardDataAccess)
            ;
        }

        public void Configure(EntityTypeBuilder<BoardColumnDtoDataAccess> builder)
        {
            builder.ToTable("BoardColumn", WorkBoardContext.WorkBoardSchema);
            builder.Ignore(m => m.Board);
            builder.HasOne(m => m.BoardDataAccess)
                .WithMany()
                .IsRequired()
                .HasForeignKey("BoardId")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}