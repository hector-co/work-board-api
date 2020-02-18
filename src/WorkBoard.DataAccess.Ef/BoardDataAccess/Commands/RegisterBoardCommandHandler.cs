using System;
using System.Threading;
using System.Threading.Tasks;
using WorkBoard.Commands.BoardCommands;

namespace WorkBoard.DataAccess.Ef.BoardDataAccess.Commands
{
    public class RegisterBoardCommandHandler : IRegisterBoardCommandHandler
    {
        private readonly WorkBoardContext _context;

        public RegisterBoardCommandHandler(WorkBoardContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(RegisterBoardCommand request, CancellationToken cancellationToken)
        {
            var boardDto = new BoardDtoDataAccess
            {
                Title = request.Title,
                Description = request.Description,
                Version = 1,
                Guid = Guid.NewGuid()
            };
            _context.Set<BoardDtoDataAccess>().Add(boardDto);

            await _context.SaveChangesAsync(cancellationToken);

            return boardDto.Id;
        }
    }
}
