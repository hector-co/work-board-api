using System;
using System.Collections.Generic;

namespace WorkBoard.Application.Dtos
{
    public class UserDto
    {
		public UserDto()
		{
		}

        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public bool Veryfied { get; set; }

		public int Version { get; set; }

		public Guid Guid { get; set; }
    }
}