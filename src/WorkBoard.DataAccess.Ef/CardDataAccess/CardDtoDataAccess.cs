using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using WorkBoard.DataAccess.Ef.BoardDataAccess;
using WorkBoard.DataAccess.Ef.BoardColumnDataAccess;
using WorkBoard.DataAccess.Ef.UserDataAccess;
using WorkBoard.Dtos;

namespace WorkBoard.DataAccess.Ef.CardDataAccess
{
	public class CardDtoDataAccess : CardDto
    {
        public CardDtoDataAccess()
        {
        }

		public BoardDtoDataAccess BoardDataAccess { get; set; }

		public BoardColumnDtoDataAccess ColumnDataAccess { get; set; }

		public List<CardDtoDataAccessUserDto> OwnersDataAccess { get; set; }

    }
}
