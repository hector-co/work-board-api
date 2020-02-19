using MediatR;

namespace WorkBoard.Commands.BoardColumnCommands
{
    public interface IReOpenBoardCommandHandler : IRequestHandler<ReOpenBoardCommand>
    {
    }
}
