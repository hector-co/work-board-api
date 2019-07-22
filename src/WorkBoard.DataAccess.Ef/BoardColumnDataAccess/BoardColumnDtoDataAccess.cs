using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using WorkBoard.DataAccess.Ef.BoardDataAccess;
using WorkBoard.Application.Dtos;

namespace WorkBoard.DataAccess.Ef.BoardColumnDataAccess
{
	public class BoardColumnDtoDataAccess : BoardColumnDto
    {
        public BoardColumnDtoDataAccess()
        {
        }

		public BoardDtoDataAccess BoardDataAccess { get; set; }

    }
}
