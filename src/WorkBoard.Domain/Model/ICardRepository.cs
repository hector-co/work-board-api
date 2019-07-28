using System;
using Hco.Base.Domain;

namespace WorkBoard.Domain.Model
{
    public interface ICardRepository : IRepository<Card, int>
    {
    }
}
