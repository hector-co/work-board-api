using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WorkBoard.Commands.BoardColumnCommands;
using WorkBoard.Commands.Exceptions;
using WorkBoard.DataAccess.Ef.BoardDataAccess;
using WorkBoard.Dtos;

namespace WorkBoard.DataAccess.Ef.BoardColumnDataAccess.Commands
{
    public class EditColumnCommandHandler : IEditColumnCommandHandler
    {
        private readonly WorkBoardContext _context;

        public EditColumnCommandHandler(WorkBoardContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(EditColumnCommand request, CancellationToken cancellationToken)
        {
            var boardDto = await _context.Set<BoardDtoDataAccess>().FindAsync(request.BoardId);
            if (boardDto == null || boardDto.State == BoardState.Closed) throw new CommandException();

            var columnDto = _context.Set<BoardColumnDtoDataAccess>().Find(request.ColumnId);
            columnDto.Title = request.Title;
            columnDto.Version++;

            await _context.SaveChangesAsync();

            return await Unit.Task;
        }
    }
}
