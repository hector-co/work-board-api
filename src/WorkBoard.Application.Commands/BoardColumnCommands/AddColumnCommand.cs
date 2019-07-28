using MediatR;
using Newtonsoft.Json;

namespace WorkBoard.Application.Commands.BoardColumnCommands
{
    public class AddColumnCommand : IRequest
    {
        [JsonIgnore]
        public int BoardId { get; set; }

        public string Title { get; set; }
    }
}
