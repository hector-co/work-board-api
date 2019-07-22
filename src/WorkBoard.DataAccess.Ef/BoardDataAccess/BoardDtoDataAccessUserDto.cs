using System;
using WorkBoard.Application.Dtos;

namespace WorkBoard.DataAccess.Ef.BoardDataAccess
{
	public class BoardDtoDataAccessUserDto
    {
        public int UserId { get; set; }
		public UserDto User { get; set; }
		public int BoardId { get; set; }
		public BoardDtoDataAccess Board { get; set; }
    }
}
