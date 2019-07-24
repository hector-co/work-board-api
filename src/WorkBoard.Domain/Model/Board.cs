using System;
using System.Collections.Generic;
using Hco.Base.Domain;

namespace WorkBoard.Domain.Model
{
    public class Board : AggregateRoot<int>
    {
        private string _title;
        private List<int> _usersId;
        private List<BoardColumn> _columns;
        private string _description;
        private int _state;

		protected Board()
		{
			_usersId = new List<int>();
			_columns = new List<BoardColumn>();
		}
    }
}
