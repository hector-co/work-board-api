using MediatR;

namespace WorkBoard.Application.Commands.CardCommands
{
    public interface IEditCardCommandHandler : IRequestHandler<EditCardCommand>
    {
    }
}
