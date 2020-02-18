using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WorkBoard.Commands.BoardColumnCommands;

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
            var columnDto = _context.Set<BoardColumnDtoDataAccess>().Find(request.ColumnId);
            columnDto.Title = request.Title;
            columnDto.Version++;

            await _context.SaveChangesAsync();

            return await Unit.Task;
        }
    }
}
