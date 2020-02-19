using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WorkBoard.Commands.BoardColumnCommands;
using WorkBoard.Commands.Exceptions;

namespace WorkBoard.DataAccess.Ef.BoardDataAccess.Commands
{
    public class ReOpenBoardCommandHandler : IReOpenBoardCommandHandler
    {
        private readonly WorkBoardContext _context;

        public ReOpenBoardCommandHandler(WorkBoardContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(ReOpenBoardCommand request, CancellationToken cancellationToken)
        {
            var board = await _context.Set<BoardDtoDataAccess>().FindAsync(request.BoardId);
            if (board == null) throw new CommandException();

            board.State = Dtos.BoardState.Open;

            await _context.SaveChangesAsync();

            return await Unit.Task;
        }
    }
}
