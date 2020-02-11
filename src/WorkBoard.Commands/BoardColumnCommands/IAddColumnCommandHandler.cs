using MediatR;

namespace WorkBoard.Commands.BoardColumnCommands
{
    public interface IAddColumnCommandHandler : IRequestHandler<AddColumnCommand, int>
    {
    }
}
