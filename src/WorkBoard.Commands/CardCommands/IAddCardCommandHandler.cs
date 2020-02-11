using MediatR;

namespace WorkBoard.Commands.CardCommands
{
    public interface IAddCardCommandHandler : IRequestHandler<AddCardCommand, int>
    {
    }
}
