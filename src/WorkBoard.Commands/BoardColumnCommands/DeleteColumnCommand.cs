using MediatR;
using Newtonsoft.Json;

namespace WorkBoard.Commands.BoardColumnCommands
{
    public class DeleteColumnCommand : IRequest
    {
        [JsonIgnore]
        public int BoardId { get; set; }
        [JsonIgnore]
        public int ColumnId { get; set; }
    }
}
