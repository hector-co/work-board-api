using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkBoard.Commands.CardCommands;
using WorkBoard.Commands.Exceptions;
using WorkBoard.DataAccess.Ef.BoardColumnDataAccess;
using WorkBoard.Dtos;

namespace WorkBoard.DataAccess.Ef.CardDataAccess.Commands
{
    public class MoveCardCommandHandler : IMoveCardCommandHandler
    {
        private readonly WorkBoardContext _context;

        public MoveCardCommandHandler(WorkBoardContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(MoveCardCommand request, CancellationToken cancellationToken)
        {
            var cardDto = _context.Set<CardDtoDataAccess>().Include(c => c.BoardDataAccess).Include(c => c.ColumnDataAccess).FirstOrDefault(c => c.Id == request.CardId);

            if (cardDto == null) throw new CommandException();
            if (cardDto.BoardDataAccess == null || cardDto.BoardDataAccess.State == BoardState.Closed) throw new CommandException();

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
