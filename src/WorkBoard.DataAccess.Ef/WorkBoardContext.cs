using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WorkBoard.DataAccess.Ef
{
    public class WorkBoardContext : DbContext
    {
        public const string WorkBoardSchema = "dbo";

        public WorkBoardContext(DbContextOptions options) : base(options)
        {
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
