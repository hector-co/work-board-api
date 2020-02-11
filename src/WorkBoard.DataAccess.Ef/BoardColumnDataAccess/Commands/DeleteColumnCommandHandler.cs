using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hco.Base.DataAccess.Ef;
using Hco.Base.Domain;
using MediatR;
using WorkBoard.Commands.BoardColumnCommands;
using WorkBoard.Commands.Exceptions;
using WorkBoard.DataAccess.Ef.CardDataAccess;

namespace WorkBoard.DataAccess.Ef.BoardColumnDataAccess.Commands
{
    public class DeleteColumnCommandHandler : IDeleteColumnCommandHandler
    {
        private readonly WorkBoardContext _context;

        public DeleteColumnCommandHandler(IUnitOfWork unitOfWork)
        {
            _context = ((UnitOfWorkEf<WorkBoardContext>)unitOfWork).CurrentContext;
        }

        public async Task<Unit> Handle(DeleteColumnCommand request, CancellationToken cancellationToken)
        {
            if (_context.Set<CardDtoDataAccess>().Any(c => c.ColumnDataAccess.Id == request.ColumnId))
                throw new CommandException();

            var columnDto = _context.Set<BoardColumnDtoDataAccess>().Find(request.ColumnId);

            _context.Set<BoardColumnDtoDataAccess>().Remove(columnDto);
            return await Unit.Task;
        }
    }
}
