using MediatR;
using Newtonsoft.Json;

namespace WorkBoard.Commands.BoardColumnCommands
{
    public class AddColumnCommand : IRequest<int>
    {
        [JsonIgnore]
        public int BoardId { get; set; }

        public string Title { get; set; }
    }
}
