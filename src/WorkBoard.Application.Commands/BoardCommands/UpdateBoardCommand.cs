﻿using MediatR;
using Newtonsoft.Json;

namespace WorkBoard.Application.Commands.BoardCommands
{
    public class UpdateBoardCommand : IRequest
    {
        [JsonIgnore]
        public int BoardId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
