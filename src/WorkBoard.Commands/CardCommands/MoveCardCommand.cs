using MediatR;
using System.Text.Json.Serialization;

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
