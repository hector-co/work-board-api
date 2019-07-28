using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hco.Base.DataAccess.Ef;
using Hco.Base.Domain;
using MediatR;
using WorkBoard.Application.Commands.BoardColumnCommands;
using WorkBoard.DataAccess.Ef.BoardDataAccess;

namespace WorkBoard.DataAccess.Ef.BoardColumnDataAccess.Commands
{
    public class AddColumnCommandHandler : IAddColumnCommandHandler
    {
        private readonly WorkBoardContext _context;

        public AddColumnCommandHandler(IUnitOfWork unitOfWork)
        {
            _context = ((UnitOfWorkEf<WorkBoardContext>)unitOfWork).CurrentContext;
        }

        public async Task<Unit> Handle(AddColumnCommand request, CancellationToken cancellationToken)
        {
            var maxOrder = 1;
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

            return await Unit.Task;
        }
    }
}
