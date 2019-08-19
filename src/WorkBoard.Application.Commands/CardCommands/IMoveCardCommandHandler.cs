using MediatR;

namespace WorkBoard.Application.Commands.CardCommands
{
    public interface IMoveCardCommandHandler : IRequestHandler<MoveCardCommand>
    {
    }
}
