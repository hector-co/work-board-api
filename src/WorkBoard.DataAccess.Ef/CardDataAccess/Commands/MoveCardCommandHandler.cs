﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hco.Base.DataAccess.Ef;
using Hco.Base.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkBoard.Application.Commands.CardCommands;
using WorkBoard.DataAccess.Ef.BoardColumnDataAccess;

namespace WorkBoard.DataAccess.Ef.CardDataAccess.Commands
{
    public class MoveCardCommandHandler : IMoveCardCommandHandler
    {
        private readonly WorkBoardContext _context;

        public MoveCardCommandHandler(IUnitOfWork unitOfWork)
        {
            _context = ((UnitOfWorkEf<WorkBoardContext>)unitOfWork).CurrentContext;
        }

        public async Task<Unit> Handle(MoveCardCommand request, CancellationToken cancellationToken)
        {
            var cardDto = _context.Set<CardDtoDataAccess>().Include(c => c.ColumnDataAccess).FirstOrDefault(c => c.Id == request.CardId);

            if (cardDto.ColumnDataAccess.Id == request.ColumnId && cardDto.Order == request.Order)
                return await Unit.Task;

            var sourceColumnCards = _context.Set<CardDtoDataAccess>().Where(c => c.ColumnDataAccess.Id == cardDto.ColumnDataAccess.Id && c.Order >= cardDto.Order);

            foreach (var card in sourceColumnCards)
            {
                card.Order--;
            }

            var targetColumnCards = _context.Set<CardDtoDataAccess>().Where(c => c.ColumnDataAccess.Id == request.ColumnId && c.Order >= cardDto.Order);

            foreach (var card in targetColumnCards)
            {
                card.Order++;
            }

            var targetColumnDto = _context.Set<BoardColumnDtoDataAccess>().FirstOrDefault(c => c.Id == request.ColumnId);
            cardDto.Order = request.Order;
            cardDto.ColumnDataAccess = targetColumnDto;

            await _context.SaveChangesAsync();

            return await Unit.Task;
        }
    }
}
