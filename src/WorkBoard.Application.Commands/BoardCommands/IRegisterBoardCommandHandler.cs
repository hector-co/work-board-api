using MediatR;

namespace WorkBoard.Application.Commands.BoardCommands
{
    public interface IRegisterBoardCommandHandler : IRequestHandler<RegisterBoardCommand>
    {
    }
}
