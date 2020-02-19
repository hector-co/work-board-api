using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkBoard.Commands.CardCommands;
using WorkBoard.Commands.Exceptions;
using WorkBoard.Dtos;

namespace WorkBoard.DataAccess.Ef.CardDataAccess.Commands
{
    public class UpdateCardPointsCommandHandler : IUpdateCardPointsCommandHandler
    {
        private readonly WorkBoardContext _context;

        public UpdateCardPointsCommandHandler(WorkBoardContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateCardPointsCommand request, CancellationToken cancellationToken)
        {
            var cardDto = _context.Set<CardDtoDataAccess>().Include(c => c.BoardDataAccess).FirstOrDefault(c => c.Id == request.CardId);

            if (cardDto == null) throw new CommandException();
            if (cardDto.BoardDataAccess == null || cardDto.BoardDataAccess.State == BoardState.Closed) throw new CommandException();

            cardDto.EstimatedPoints = request.EstimatedPoints;
            cardDto.ConsumedPoints = request.ConsumedPoints;

            await _context.SaveChangesAsync();

            return await Unit.Task;
        }
    }
}
