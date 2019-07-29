using MediatR;

namespace WorkBoard.Application.Commands.CardCommands
{
    public interface IAddCardCommandHandler : IRequestHandler<AddCardCommand, int>
    {
    }
}
