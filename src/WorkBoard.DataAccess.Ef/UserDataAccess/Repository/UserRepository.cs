using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Hco.Base.Domain;
using Hco.Base.DataAccess.Exceptions;
using Hco.Base.DataAccess;
using Hco.Base.DataAccess.Ef;
using WorkBoard.Domain.Model;
using WorkBoard.Dtos;

namespace WorkBoard.DataAccess.Ef.UserDataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
		private readonly UnitOfWorkEf<WorkBoardContext> _unitOfWork;

        public UserRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = (UnitOfWorkEf<WorkBoardContext>)unitOfWork;
        }

		public User GetById(int id)
        {
            var ctx = _unitOfWork.CurrentContext;
			var dataAccessObject = ctx.Set<UserDto>()
                .AsNoTracking()
                .FirstOrDefault(m => m.Id == id);
            if (dataAccessObject == null) return null;
            var user = UserDataAccessMapper.Map(ctx, dataAccessObject);
            return user;
        }

		public User GetById(int id, int version)
        {
            throw new NotSupportedException();
        }

		public User GetByGuid(Guid guid)
        {
            var ctx = _unitOfWork.CurrentContext;
			var dataAccessObject = ctx.Set<UserDto>()
                .AsNoTracking()
                .FirstOrDefault(m => m.Guid == guid);
            if (dataAccessObject == null) return null;
            var user = UserDataAccessMapper.Map(ctx, dataAccessObject);
            return user;
        }

		public User GetByGuid(Guid guid, int version)
        {
            throw new NotSupportedException();
        }

        public void Save(User aggregate)
        {
            var ctx = _unitOfWork.CurrentContext;
            var dataAccessObject = UserDataAccessMapper.Map(ctx, aggregate);
			ctx.Set<UserDto>().Add(dataAccessObject);
            _unitOfWork.TrackAggregate(aggregate, () => aggregate.SetPropertyValue("Id", dataAccessObject.Id));
        }

        public void Update(int id, User aggregate)
        {
            var ctx = _unitOfWork.CurrentContext;
            if (GetVersion(id) != aggregate.AggregateVersion)
            {
                throw new AggregateVersionException();
            }
			aggregate.SetPropertyValue("Id", id);
			var dataAccessObject = ctx.Set<UserDto>()
                .FirstOrDefault(m => m.Id == id);
            aggregate.AggregateVersion++;
            UserDataAccessMapper.Map(ctx, aggregate, ref dataAccessObject);
			_unitOfWork.TrackAggregate(aggregate);
        }

        public int GetVersion(int id)
        {
            var ctx = _unitOfWork.CurrentContext;
            return ctx.Set<UserDto>().FirstOrDefault(m => m.Id == id)?.Version ?? 0;
        }
    }
}
