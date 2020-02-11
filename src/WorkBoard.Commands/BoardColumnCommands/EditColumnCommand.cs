using MediatR;
using Newtonsoft.Json;

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
