﻿using MediatR;
using System.Text.Json.Serialization;

namespace WorkBoard.Commands.BoardCommands
{
    public class ReOpenBoardCommand : IRequest
    {
        [JsonIgnore]
        public int BoardId { get; set; }
    }
}
