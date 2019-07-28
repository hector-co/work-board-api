using System;
using System.Collections.Generic;

namespace WorkBoard.Application.Dtos
{
    public class BoardColumnDto
    {
		public BoardColumnDto()
		{
		}

        public int Id { get; set; }

        public BoardDto Board { get; set; }

        public string Title { get; set; }

        public int Order { get; set; }

        public string Description { get; set; }

        public bool Active { get; set; }

		public int Version { get; set; }

		public Guid Guid { get; set; }
    }
}
