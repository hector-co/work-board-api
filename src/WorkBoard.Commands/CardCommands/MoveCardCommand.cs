using MediatR;
using Newtonsoft.Json;

namespace WorkBoard.Commands.CardCommands
{
    public class MoveCardCommand : IRequest
    {
        [JsonIgnore]
        public int CardId { get; set; }
        public int ColumnId { get; set; }
        public int Order { get; set; }
    }
}
