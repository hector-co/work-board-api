using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hco.Base.DataAccess.Ef;
using Hco.Base.Domain;
using MediatR;
using WorkBoard.Commands.CardCommands;
using WorkBoard.Dtos;

namespace WorkBoard.DataAccess.Ef.CardDataAccess.Commands
{
    public class EditCardCommandHandler : IEditCardCommandHandler
    {
        private readonly WorkBoardContext _context;

        public EditCardCommandHandler(IUnitOfWork unitOfWork)
        {
            _context = ((UnitOfWorkEf<WorkBoardContext>)unitOfWork).CurrentContext;
        }

        public async Task<Unit> Handle(EditCardCommand request, CancellationToken cancellationToken)
        {
            var cardDto = _context.Set<CardDtoDataAccess>().FirstOrDefault(c => c.Id == request.CardId);
            cardDto.Title = request.Title;
            cardDto.Description = request.Description;
            cardDto.Color = request.Color;
            cardDto.EstimatedPoints = request.EstimatedPoints;
            cardDto.Priority = (CardPriority)request.Priority;

            await _context.SaveChangesAsync(cancellationToken);
            return await Unit.Task;
        }
    }
}
