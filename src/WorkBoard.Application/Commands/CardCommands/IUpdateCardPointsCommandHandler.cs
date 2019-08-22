using MediatR;

namespace WorkBoard.Application.Commands.CardCommands
{
    public interface IUpdateCardPointsCommandHandler : IRequestHandler<UpdateCardPointsCommand>
    {
    }
}
