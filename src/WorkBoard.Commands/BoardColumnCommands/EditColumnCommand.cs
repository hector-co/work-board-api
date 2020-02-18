using MediatR;
using System.Text.Json.Serialization;

namespace WorkBoard.Commands.BoardColumnCommands
{
    public class EditColumnCommand : IRequest
    {
        [JsonIgnore]
        public int BoardId { get; set; }
        [JsonIgnore]
        public int ColumnId { get; set; }
        public string Title { get; set; }
    }
}
