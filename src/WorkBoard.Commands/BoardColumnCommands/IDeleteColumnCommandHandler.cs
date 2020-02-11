using MediatR;

namespace WorkBoard.Commands.BoardColumnCommands
{
    public interface IDeleteColumnCommandHandler : IRequestHandler<DeleteColumnCommand>
    {
    }
}
