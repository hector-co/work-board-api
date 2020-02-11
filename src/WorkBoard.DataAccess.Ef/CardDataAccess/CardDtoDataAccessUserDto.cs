using System;
using WorkBoard.Dtos;

namespace WorkBoard.DataAccess.Ef.CardDataAccess
{
	public class CardDtoDataAccessUserDto
    {
        public int UserId { get; set; }
		public UserDto User { get; set; }
		public int CardId { get; set; }
		public CardDtoDataAccess Card { get; set; }
    }
}
