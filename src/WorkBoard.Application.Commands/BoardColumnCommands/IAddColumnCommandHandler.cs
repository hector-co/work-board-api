using MediatR;

namespace WorkBoard.Application.Commands.BoardColumnCommands
{
    public interface IAddColumnCommandHandler : IRequestHandler<AddColumnCommand>
    {
    }
}
