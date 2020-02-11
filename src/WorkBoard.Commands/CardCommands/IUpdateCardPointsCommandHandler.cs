using MediatR;

namespace WorkBoard.Commands.CardCommands
{
    public interface IUpdateCardPointsCommandHandler : IRequestHandler<UpdateCardPointsCommand>
    {
    }
}
