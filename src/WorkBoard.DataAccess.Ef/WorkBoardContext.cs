using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using WorkBoard.DataAccess.Ef.UserDataAccess;
using WorkBoard.DataAccess.Ef.BoardDataAccess;
using WorkBoard.DataAccess.Ef.BoardColumnDataAccess;
using WorkBoard.Application.Dtos;

namespace WorkBoard.DataAccess.Ef
{
	public class WorkBoardContext : DbContext
	{
		public const string WorkBoardSchema = "workBoard";

		public WorkBoardContext(DbContextOptions options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserDtoConfiguration());
            modelBuilder.ApplyConfiguration(new BoardDtoDataAccessConfiguration());
            modelBuilder.ApplyConfiguration(new BoardDtoDataAccessUserDtoConfiguration());
            modelBuilder.ApplyConfiguration(new BoardColumnDtoDataAccessConfiguration());
        }
	}

    internal class WorkBoardContextFactory : IDesignTimeDbContextFactory<WorkBoardContext>
    {
        public WorkBoardContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WorkBoardContext>();
            optionsBuilder.UseSqlServer("Server=.;Database=WorkBoard;Integrated Security=True;MultipleActiveResultSets=true");

            return new WorkBoardContext(optionsBuilder.Options);
        }
    }
}
