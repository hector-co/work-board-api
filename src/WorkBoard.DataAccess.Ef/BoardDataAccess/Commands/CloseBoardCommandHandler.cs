using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WorkBoard.Commands.BoardColumnCommands;
using WorkBoard.Commands.Exceptions;

namespace WorkBoard.DataAccess.Ef.BoardDataAccess.Commands
{
    public class CloseBoardCommandHandler : ICloseBoardCommandHandler
    {
        private readonly WorkBoardContext _context;

        public CloseBoardCommandHandler(WorkBoardContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CloseBoardCommand request, CancellationToken cancellationToken)
        {
            var board = await _context.Set<BoardDtoDataAccess>().FindAsync(request.BoardId);
            if (board == null) throw new CommandException();

            board.State = Dtos.BoardState.Closed;

            await _context.SaveChangesAsync();

            return await Unit.Task;
        }
    }
}
