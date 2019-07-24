using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkBoard.Application.Dtos;

namespace WorkBoard.DataAccess.Ef.BoardDataAccess
{
    public class BoardColumnDtoConfiguration : IEntityTypeConfiguration<BoardColumnDto>
    {
        public void Configure(EntityTypeBuilder<BoardColumnDto> builder)
        {
            builder.ToTable("BoardColumn", WorkBoardContext.WorkBoardSchema);
        }
    }
}