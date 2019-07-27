using MediatR;

namespace WorkBoard.Application.Commands.BoardCommands
{
    public interface IUpdateBoardCommandHandler : IRequestHandler<UpdateBoardCommand>
    {
    }
}
