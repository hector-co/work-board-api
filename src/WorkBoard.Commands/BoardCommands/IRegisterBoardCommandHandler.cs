using MediatR;

namespace WorkBoard.Commands.BoardCommands
{
    public interface IRegisterBoardCommandHandler : IRequestHandler<RegisterBoardCommand, int>
    {
    }
}
