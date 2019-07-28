using System.Threading;
using System.Threading.Tasks;
using Hco.Base.DataAccess.Ef;
using Hco.Base.Domain;
using MediatR;
using WorkBoard.Application.Commands.BoardColumnCommands;

namespace WorkBoard.DataAccess.Ef.BoardColumnDataAccess.Commands
{
    public class EditColumnCommandHandler : IEditColumnCommandHandler
    {
        private readonly WorkBoardContext _context;

        public EditColumnCommandHandler(IUnitOfWork unitOfWork)
        {
            _context = ((UnitOfWorkEf<WorkBoardContext>)unitOfWork).CurrentContext;
        }

        public async Task<Unit> Handle(EditColumnCommand request, CancellationToken cancellationToken)
        {
            var columnDto = _context.Set<BoardColumnDtoDataAccess>().Find(request.ColumnId);
            columnDto.Title = request.Title;
            columnDto.Version++;
            return await Unit.Task;
        }
    }
}
