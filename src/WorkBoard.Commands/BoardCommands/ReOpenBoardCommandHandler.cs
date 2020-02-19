using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WorkBoard.Commands.Exceptions;
using WorkBoard.Domain.Model;

namespace WorkBoard.Commands.BoardCommands
{
    public class ReOpenBoardCommandHandler : IRequestHandler<ReOpenBoardCommand>
    {
        private readonly IBoardRepository _boardRepository;

        public ReOpenBoardCommandHandler(IBoardRepository boardRepository)
        {
            _boardRepository = boardRepository;
        }

        public async Task<Unit> Handle(ReOpenBoardCommand request, CancellationToken cancellationToken)
        {
            var board = _boardRepository.GetById(request.BoardId);
            if (board == null) throw new CommandException();

            board.ReOpen();
            _boardRepository.Update(board.Id, board);

            return await Unit.Task;
        }
    }
}
