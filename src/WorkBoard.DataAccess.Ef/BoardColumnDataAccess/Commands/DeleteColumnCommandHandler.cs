using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WorkBoard.Commands.BoardColumnCommands;
using WorkBoard.Commands.Exceptions;
using WorkBoard.DataAccess.Ef.BoardDataAccess;
using WorkBoard.DataAccess.Ef.CardDataAccess;
using WorkBoard.Dtos;

namespace WorkBoard.DataAccess.Ef.BoardColumnDataAccess.Commands
{
    public class DeleteColumnCommandHandler : IDeleteColumnCommandHandler
    {
        private readonly WorkBoardContext _context;

        public DeleteColumnCommandHandler(WorkBoardContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteColumnCommand request, CancellationToken cancellationToken)
        {
            var boardDto = await _context.Set<BoardDtoDataAccess>().FindAsync(request.BoardId);
            if (boardDto == null || boardDto.State == BoardState.Closed) throw new CommandException();

            if (_context.Set<CardDtoDataAccess>().Any(c => c.ColumnDataAccess.Id == request.ColumnId))
                throw new CommandException();

            var columnDto = _context.Set<BoardColumnDtoDataAccess>().Find(request.ColumnId);

            _context.Set<BoardColumnDtoDataAccess>().Remove(columnDto);

            await _context.SaveChangesAsync();

            return await Unit.Task;
        }
    }
}
