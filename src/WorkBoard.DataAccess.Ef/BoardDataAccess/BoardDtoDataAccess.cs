using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using WorkBoard.DataAccess.Ef.UserDataAccess;
using WorkBoard.Application.Dtos;

namespace WorkBoard.DataAccess.Ef.BoardDataAccess
{
	public class BoardDtoDataAccess : BoardDto
    {
        public BoardDtoDataAccess()
        {
        }

		public List<BoardDtoDataAccessUserDto> UsersDataAccess { get; set; }

    }
}
