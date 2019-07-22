using System;
using Hco.Base.Domain;

namespace WorkBoard.Domain.Model
{
    public interface IBoardRepository : IRepository<Board, int>
    {
    }
}
