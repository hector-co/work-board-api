using Microsoft.EntityFrameworkCore;
using System.Linq;
using WorkBoard.Dtos;

namespace WorkBoard.DataAccess.Ef.BoardColumnDataAccess.Queries
{
    internal static class BoardColumnDtoQueryHandlerExtensions
    {
        public static IQueryable<BoardColumnDtoDataAccess> AddIncludes
            (this IQueryable<BoardColumnDtoDataAccess> queryable)
        {
            return queryable
                .Include(m => m.BoardDataAccess)
                ;
        }
    }
}
