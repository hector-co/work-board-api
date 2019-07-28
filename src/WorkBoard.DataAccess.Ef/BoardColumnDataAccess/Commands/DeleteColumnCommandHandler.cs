using System.Threading;
using System.Threading.Tasks;
using Hco.Base.DataAccess.Ef;
using Hco.Base.Domain;
using MediatR;
using WorkBoard.Application.Commands.BoardColumnCommands;

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
            // TODO validate column can not be removed if contains cards
            var columnDto = _context.Set<BoardColumnDtoDataAccess>().Find(request.ColumnId);
            _context.Set<BoardColumnDtoDataAccess>().Remove(columnDto);
            return await Unit.Task;
        }
    }
}
