using MediatR;

namespace WorkBoard.Commands.CardCommands
{
    public interface IMoveCardCommandHandler : IRequestHandler<MoveCardCommand>
    {
    }
}
