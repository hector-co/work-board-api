using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Hco.Base.Domain;
using Hco.Base.DataAccess.Exceptions;
using Hco.Base.DataAccess;
using Hco.Base.DataAccess.Ef;
using WorkBoard.Domain.Model;
using WorkBoard.Application.Dtos;

namespace WorkBoard.DataAccess.Ef.BoardDataAccess.Repository
{
    public class BoardRepository : IBoardRepository
    {
		private readonly UnitOfWorkEf<WorkBoardContext> _unitOfWork;

        public BoardRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = (UnitOfWorkEf<WorkBoardContext>)unitOfWork;
        }

		public Board GetById(int id)
        {
            var ctx = _unitOfWork.CurrentContext;
			var dataAccessObject = ctx.Set<BoardDtoDataAccess>()
                .Include(m => m.Columns)
                .Include(m => m.UsersDataAccess).ThenInclude(r => r.User)
                .AsNoTracking()
                .FirstOrDefault(m => m.Id == id);
            if (dataAccessObject == null) return null;
            var board = BoardDataAccessMapper.Map(ctx, dataAccessObject);
            return board;
        }

		public Board GetById(int id, int version)
        {
            throw new NotSupportedException();
        }

		public Board GetByGuid(Guid guid)
        {
            var ctx = _unitOfWork.CurrentContext;
			var dataAccessObject = ctx.Set<BoardDtoDataAccess>()
                .Include(m => m.Columns)
                .Include(m => m.UsersDataAccess).ThenInclude(r => r.User)
                .AsNoTracking()
                .FirstOrDefault(m => m.Guid == guid);
            if (dataAccessObject == null) return null;
            var board = BoardDataAccessMapper.Map(ctx, dataAccessObject);
            return board;
        }

		public Board GetByGuid(Guid guid, int version)
        {
            throw new NotSupportedException();
        }

        public void Save(Board aggregate)
        {
            var ctx = _unitOfWork.CurrentContext;
            var dataAccessObject = BoardDataAccessMapper.Map(ctx, aggregate);
			ctx.Set<BoardDtoDataAccess>().Add(dataAccessObject);
            _unitOfWork.TrackAggregate(aggregate, () => aggregate.SetPropertyValue("Id", dataAccessObject.Id));
        }

        public void Update(int id, Board aggregate)
        {
            var ctx = _unitOfWork.CurrentContext;
            if (GetVersion(id) != aggregate.AggregateVersion)
            {
                throw new AggregateVersionException();
            }
			aggregate.SetPropertyValue("Id", id);
			var dataAccessObject = ctx.Set<BoardDtoDataAccess>()
                .Include(m => m.Columns)
                .Include(m => m.UsersDataAccess).ThenInclude(r => r.User)
                .FirstOrDefault(m => m.Id == id);
            aggregate.AggregateVersion++;
            BoardDataAccessMapper.Map(ctx, aggregate, ref dataAccessObject);
			_unitOfWork.TrackAggregate(aggregate);
        }

        public int GetVersion(int id)
        {
            var ctx = _unitOfWork.CurrentContext;
            return ctx.Set<BoardDtoDataAccess>().FirstOrDefault(m => m.Id == id)?.Version ?? 0;
        }
    }
}
