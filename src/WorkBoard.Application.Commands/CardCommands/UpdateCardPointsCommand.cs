using MediatR;
using Newtonsoft.Json;

namespace WorkBoard.Application.Commands.CardCommands
{
    public class UpdateCardPointsCommand : IRequest
    {
        [JsonIgnore]
        public int CardId { get; set; }
        public float EstimatedPoints { get; set; }
        public float ConsumedPoints { get; set; }
    }
}
