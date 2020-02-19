using MediatR;

namespace WorkBoard.Commands.BoardColumnCommands
{
    public interface ICloseBoardCommandHandler : IRequestHandler<CloseBoardCommand>
    {
    }
}
