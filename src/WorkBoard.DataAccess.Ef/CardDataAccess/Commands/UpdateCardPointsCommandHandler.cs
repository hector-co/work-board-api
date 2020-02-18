using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WorkBoard.Commands.CardCommands;

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
            var cardDto = _context.Set<CardDtoDataAccess>().FirstOrDefault(c => c.Id == request.CardId);
            cardDto.EstimatedPoints = request.EstimatedPoints;
            cardDto.ConsumedPoints = request.ConsumedPoints;

            await _context.SaveChangesAsync();

            return await Unit.Task;
        }
    }
}
