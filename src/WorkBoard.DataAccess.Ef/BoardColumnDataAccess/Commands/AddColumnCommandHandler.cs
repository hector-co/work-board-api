using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WorkBoard.Commands.BoardColumnCommands;
using WorkBoard.DataAccess.Ef.BoardDataAccess;

namespace WorkBoard.DataAccess.Ef.BoardColumnDataAccess.Commands
{
    public class AddColumnCommandHandler : IAddColumnCommandHandler
    {
        private readonly WorkBoardContext _context;

        public AddColumnCommandHandler(WorkBoardContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(AddColumnCommand request, CancellationToken cancellationToken)
        {
            var maxOrder = 0;
            if (_context.Set<BoardColumnDtoDataAccess>().Count(c => c.BoardDataAccess.Id == request.BoardId) > 0)
            {
                maxOrder = _context.Set<BoardColumnDtoDataAccess>().Where(c => c.BoardDataAccess.Id == request.BoardId).Max(c => c.Order);
            }

            var columnDto = new BoardColumnDtoDataAccess
            {
                Title = request.Title,
                BoardDataAccess = _context.Set<BoardDtoDataAccess>().FirstOrDefault(b => b.Id == request.BoardId),
                Order = maxOrder + 1,
                Active = true,
                Version = 1,
                Guid = Guid.NewGuid()
            };
            _context.Set<BoardColumnDtoDataAccess>().Add(columnDto);

            await _context.SaveChangesAsync();

            return columnDto.Id;
        }
    }
}
