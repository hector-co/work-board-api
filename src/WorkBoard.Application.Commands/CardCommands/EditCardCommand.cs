using MediatR;
using Newtonsoft.Json;

namespace WorkBoard.Application.Commands.CardCommands
{
    public class EditCardCommand : IRequest
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Color { get; set; }

        public int Priority { get; set; }

        public float EstimatedPoints { get; set; }
    }
}
