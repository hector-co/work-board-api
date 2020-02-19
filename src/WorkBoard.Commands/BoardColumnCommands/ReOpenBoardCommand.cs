using MediatR;
using System.Text.Json.Serialization;

namespace WorkBoard.Commands.BoardColumnCommands
{
    public class CloseBoardCommand : IRequest
    {
        [JsonIgnore]
        public int BoardId { get; set; }
    }
}
