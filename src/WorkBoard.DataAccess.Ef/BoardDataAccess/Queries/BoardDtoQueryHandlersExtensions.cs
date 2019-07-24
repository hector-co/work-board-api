using Microsoft.EntityFrameworkCore;
using System.Linq;
using WorkBoard.Application.Dtos;

namespace WorkBoard.DataAccess.Ef.BoardDataAccess.Queries
{
    internal static class BoardDtoQueryHandlerExtensions
    {
        public static IQueryable<BoardDtoDataAccess> AddIncludes
            (this IQueryable<BoardDtoDataAccess> queryable)
        {
            return queryable
                .Include(m => m.Columns)
                .Include(m => m.UsersDataAccess).ThenInclude(r => r.User)
                ;
        }
    }
}
