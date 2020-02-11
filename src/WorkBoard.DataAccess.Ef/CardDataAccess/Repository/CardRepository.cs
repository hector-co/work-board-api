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

namespace WorkBoard.DataAccess.Ef.CardDataAccess.Repository
{
    public class CardRepository : ICardRepository
    {
		private readonly UnitOfWorkEf<WorkBoardContext> _unitOfWork;

        public CardRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = (UnitOfWorkEf<WorkBoardContext>)unitOfWork;
        }

		public Card GetById(int id)
        {
            var ctx = _unitOfWork.CurrentContext;
			var dataAccessObject = ctx.Set<CardDtoDataAccess>()
                .Include(m => m.BoardDataAccess)
                .Include(m => m.ColumnDataAccess)
                .Include(m => m.OwnersDataAccess).ThenInclude(r => r.User)
                .AsNoTracking()
                .FirstOrDefault(m => m.Id == id);
            if (dataAccessObject == null) return null;
            var card = CardDataAccessMapper.Map(ctx, dataAccessObject);
            return card;
        }

		public Card GetById(int id, int version)
        {
            throw new NotSupportedException();
        }

		public Card GetByGuid(Guid guid)
        {
            var ctx = _unitOfWork.CurrentContext;
			var dataAccessObject = ctx.Set<CardDtoDataAccess>()
                .Include(m => m.BoardDataAccess)
                .Include(m => m.ColumnDataAccess)
                .Include(m => m.OwnersDataAccess).ThenInclude(r => r.User)
                .AsNoTracking()
                .FirstOrDefault(m => m.Guid == guid);
            if (dataAccessObject == null) return null;
            var card = CardDataAccessMapper.Map(ctx, dataAccessObject);
            return card;
        }

		public Card GetByGuid(Guid guid, int version)
        {
            throw new NotSupportedException();
        }

        public void Save(Card aggregate)
        {
            var ctx = _unitOfWork.CurrentContext;
            var dataAccessObject = CardDataAccessMapper.Map(ctx, aggregate);
			ctx.Set<CardDtoDataAccess>().Add(dataAccessObject);
            _unitOfWork.TrackAggregate(aggregate, () => aggregate.SetPropertyValue("Id", dataAccessObject.Id));
        }

        public void Update(int id, Card aggregate)
        {
            var ctx = _unitOfWork.CurrentContext;
            if (GetVersion(id) != aggregate.AggregateVersion)
            {
                throw new AggregateVersionException();
            }
			aggregate.SetPropertyValue("Id", id);
			var dataAccessObject = ctx.Set<CardDtoDataAccess>()
                .Include(m => m.BoardDataAccess)
                .Include(m => m.ColumnDataAccess)
                .Include(m => m.OwnersDataAccess).ThenInclude(r => r.User)
                .FirstOrDefault(m => m.Id == id);
            aggregate.AggregateVersion++;
            CardDataAccessMapper.Map(ctx, aggregate, ref dataAccessObject);
			_unitOfWork.TrackAggregate(aggregate);
        }

        public int GetVersion(int id)
        {
            var ctx = _unitOfWork.CurrentContext;
            return ctx.Set<CardDtoDataAccess>().FirstOrDefault(m => m.Id == id)?.Version ?? 0;
        }
    }
}
