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

namespace WorkBoard.DataAccess.Ef.BoardColumnDataAccess.Repository
{
    public class BoardColumnRepository : IBoardColumnRepository
    {
		private readonly UnitOfWorkEf<WorkBoardContext> _unitOfWork;

        public BoardColumnRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = (UnitOfWorkEf<WorkBoardContext>)unitOfWork;
        }

		public BoardColumn GetById(int id)
        {
            var ctx = _unitOfWork.CurrentContext;
			var dataAccessObject = ctx.Set<BoardColumnDtoDataAccess>()
                .Include(m => m.BoardDataAccess)
                .AsNoTracking()
                .FirstOrDefault(m => m.Id == id);
            if (dataAccessObject == null) return null;
            var boardColumn = BoardColumnDataAccessMapper.Map(ctx, dataAccessObject);
            return boardColumn;
        }

		public BoardColumn GetById(int id, int version)
        {
            throw new NotSupportedException();
        }

		public BoardColumn GetByGuid(Guid guid)
        {
            var ctx = _unitOfWork.CurrentContext;
			var dataAccessObject = ctx.Set<BoardColumnDtoDataAccess>()
                .Include(m => m.BoardDataAccess)
                .AsNoTracking()
                .FirstOrDefault(m => m.Guid == guid);
            if (dataAccessObject == null) return null;
            var boardColumn = BoardColumnDataAccessMapper.Map(ctx, dataAccessObject);
            return boardColumn;
        }

		public BoardColumn GetByGuid(Guid guid, int version)
        {
            throw new NotSupportedException();
        }

        public void Save(BoardColumn aggregate)
        {
            var ctx = _unitOfWork.CurrentContext;
            var dataAccessObject = BoardColumnDataAccessMapper.Map(ctx, aggregate);
			ctx.Set<BoardColumnDtoDataAccess>().Add(dataAccessObject);
            _unitOfWork.TrackAggregate(aggregate, () => aggregate.SetPropertyValue("Id", dataAccessObject.Id));
        }

        public void Update(int id, BoardColumn aggregate)
        {
            var ctx = _unitOfWork.CurrentContext;
            if (GetVersion(id) != aggregate.AggregateVersion)
            {
                throw new AggregateVersionException();
            }
			aggregate.SetPropertyValue("Id", id);
			var dataAccessObject = ctx.Set<BoardColumnDtoDataAccess>()
                .Include(m => m.BoardDataAccess)
                .FirstOrDefault(m => m.Id == id);
            aggregate.AggregateVersion++;
            BoardColumnDataAccessMapper.Map(ctx, aggregate, ref dataAccessObject);
			_unitOfWork.TrackAggregate(aggregate);
        }

        public int GetVersion(int id)
        {
            var ctx = _unitOfWork.CurrentContext;
            return ctx.Set<BoardColumnDtoDataAccess>().FirstOrDefault(m => m.Id == id)?.Version ?? 0;
        }
    }
}
