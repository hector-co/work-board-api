using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkBoard.Dtos
{
    public class CardDto
    {
		public CardDto()
		{
			Owners = new List<UserDto>();
		}

        public int Id { get; set; }

        [NotMapped]
        public BoardDto Board { get; set; }

        [NotMapped]
        public BoardColumnDto Column { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Color { get; set; }

        public List<UserDto> Owners { get; set; }

        public CardPriority Priority { get; set; }

        public float EstimatedPoints { get; set; }

        public float ConsumedPoints { get; set; }

        public bool Done { get; set; }

        public int Order { get; set; }

		public int Version { get; set; }

		public Guid Guid { get; set; }
    }
}
