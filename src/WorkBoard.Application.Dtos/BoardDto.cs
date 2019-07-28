using System;
using System.Collections.Generic;

namespace WorkBoard.Application.Dtos
{
    public class BoardDto
    {
		public BoardDto()
		{
			Users = new List<UserDto>();
		}

        public int Id { get; set; }

        public string Title { get; set; }

        public List<UserDto> Users { get; set; }

        public string Description { get; set; }

        public BoardState State { get; set; }

		public int Version { get; set; }

		public Guid Guid { get; set; }
    }
}
