using MediatR;

namespace WorkBoard.Commands.BoardCommands
{
    public interface IUpdateBoardCommandHandler : IRequestHandler<UpdateBoardCommand>
    {
    }
}
