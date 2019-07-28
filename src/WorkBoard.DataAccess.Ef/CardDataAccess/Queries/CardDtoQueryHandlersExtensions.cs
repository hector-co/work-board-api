using Microsoft.EntityFrameworkCore;
using System.Linq;
using WorkBoard.Application.Dtos;

namespace WorkBoard.DataAccess.Ef.CardDataAccess.Queries
{
    internal static class CardDtoQueryHandlerExtensions
    {
        public static IQueryable<CardDtoDataAccess> AddIncludes
            (this IQueryable<CardDtoDataAccess> queryable)
        {
            return queryable
                .Include(m => m.BoardDataAccess)
                .Include(m => m.ColumnDataAccess)
                .Include(m => m.OwnersDataAccess).ThenInclude(r => r.User)
                ;
        }
    }
}
