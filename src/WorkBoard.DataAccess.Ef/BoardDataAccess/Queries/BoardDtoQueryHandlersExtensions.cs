using Microsoft.EntityFrameworkCore;
using System.Linq;
using WorkBoard.Dtos;

namespace WorkBoard.DataAccess.Ef.BoardDataAccess.Queries
{
    internal static class BoardDtoQueryHandlerExtensions
    {
        public static IQueryable<BoardDtoDataAccess> AddIncludes
            (this IQueryable<BoardDtoDataAccess> queryable)
        {
            return queryable
                .Include(m => m.UsersDataAccess).ThenInclude(r => r.User)
                ;
        }
    }
}
