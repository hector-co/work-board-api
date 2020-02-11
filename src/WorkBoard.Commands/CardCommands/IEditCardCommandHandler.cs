using MediatR;

namespace WorkBoard.Commands.CardCommands
{
    public interface IEditCardCommandHandler : IRequestHandler<EditCardCommand>
    {
    }
}
