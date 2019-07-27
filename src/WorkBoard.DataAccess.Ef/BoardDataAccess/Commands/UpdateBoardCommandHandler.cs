using System.Threading;
using System.Threading.Tasks;
using Hco.Base.DataAccess.Ef;
using Hco.Base.Domain;
using MediatR;
using WorkBoard.Application.Commands.BoardCommands;

namespace WorkBoard.DataAccess.Ef.BoardDataAccess.Commands
{
    public class UpdateBoardCommandHandler : IUpdateBoardCommandHandler
    {
        private readonly WorkBoardContext _context;

        public UpdateBoardCommandHandler(IUnitOfWork unitOfWork)
        {
            _context = ((UnitOfWorkEf<WorkBoardContext>)unitOfWork).CurrentContext;
        }

        public async Task<Unit> Handle(UpdateBoardCommand request, CancellationToken cancellationToken)
        {
            var boardDto = await _context.Set<BoardDtoDataAccess>().FindAsync(request.BoardId);
            boardDto.Title = request.Title;
            boardDto.Description = request.Description;
            return await Unit.Task;
        }
    }
}
