using MediatR;

namespace WorkBoard.Commands.BoardColumnCommands
{
    public interface IEditColumnCommandHandler : IRequestHandler<EditColumnCommand>
    {
    }
}
