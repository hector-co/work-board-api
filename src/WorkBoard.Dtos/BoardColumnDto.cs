using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkBoard.Dtos
{
    public class BoardColumnDto
    {
		public BoardColumnDto()
		{
		}

        public int Id { get; set; }

        [NotMapped]
        public BoardDto Board { get; set; }

        public string Title { get; set; }

        public int Order { get; set; }

        public string Description { get; set; }

        public bool Active { get; set; }

		public int Version { get; set; }

		public Guid Guid { get; set; }
    }
}
