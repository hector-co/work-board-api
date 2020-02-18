using MediatR;
using System.Text.Json.Serialization;

namespace WorkBoard.Commands.BoardColumnCommands
{
    public class AddColumnCommand : IRequest<int>
    {
        [JsonIgnore]
        public int BoardId { get; set; }

        public string Title { get; set; }
    }
}
