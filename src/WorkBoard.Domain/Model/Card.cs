using System;
using System.Collections.Generic;
using Hco.Base.Domain;

namespace WorkBoard.Domain.Model
{
    public class Card : AggregateRoot<int>
    {
        private int _boardId;
        private int _columnId;
        private string _title;
        private string _description;
        private int _color;
        private List<int> _ownersId;
        private int _priority;
        private float _estimatedPoints;
        private float _consumedPoints;
        private bool _done;
        private int _order;

		protected Card()
		{
			_ownersId = new List<int>();
		}
    }
}
