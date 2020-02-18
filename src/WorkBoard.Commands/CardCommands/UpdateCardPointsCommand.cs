using MediatR;
using System.Text.Json.Serialization;

namespace WorkBoard.Commands.CardCommands
{
    public class UpdateCardPointsCommand : IRequest
    {
        [JsonIgnore]
        public int CardId { get; set; }
        public float EstimatedPoints { get; set; }
        public float ConsumedPoints { get; set; }
    }
}
