using MediatR;

namespace WorkBoard.Application.Commands.BoardColumnCommands
{
    public interface IEditColumnCommandHandler : IRequestHandler<EditColumnCommand>
    {
    }
}
