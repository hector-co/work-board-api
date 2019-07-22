using System;
using Hco.Base.Domain;

namespace WorkBoard.Domain.Model
{
    public interface IUserRepository : IRepository<User, int>
    {
    }
}
