using MediatR;

namespace WorkBoard.Application.Commands.BoardColumnCommands
{
    public interface IDeleteColumnCommandHandler : IRequestHandler<DeleteColumnCommand>
    {
    }
}
