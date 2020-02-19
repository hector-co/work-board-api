using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WorkBoard.Commands.BoardCommands;
using WorkBoard.Commands.Exceptions;
using WorkBoard.Dtos;

namespace WorkBoard.DataAccess.Ef.BoardDataAccess.Commands
{
    public class UpdateBoardCommandHandler : IUpdateBoardCommandHandler
    {
        private readonly WorkBoardContext _context;

        public UpdateBoardCommandHandler(WorkBoardContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateBoardCommand request, CancellationToken cancellationToken)
        {
            var boardDto = await _context.Set<BoardDtoDataAccess>().FindAsync(request.BoardId);
            if (boardDto == null || boardDto.State == BoardState.Closed) throw new CommandException();

            boardDto.Title = request.Title;
            boardDto.Description = request.Description;
            boardDto.Version++;

            await _context.SaveChangesAsync();

            return await Unit.Task;
        }
    }
}
