using MediatR;

namespace WorkBoard.Application.Commands.BoardCommands
{
    public class RegisterBoardCommand : IRequest<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
