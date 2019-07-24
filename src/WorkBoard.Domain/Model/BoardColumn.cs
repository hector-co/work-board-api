using System;
using System.Collections.Generic;
using Hco.Base.Domain;

namespace WorkBoard.Domain.Model
{
    public class BoardColumn : Entity<int>
    {
        private string _title;
        private int _order;
        private string _description;
        private bool _active;

		protected BoardColumn()
		{
		}
    }
}
