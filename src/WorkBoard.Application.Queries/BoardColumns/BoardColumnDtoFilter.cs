using System;
using WorkBoard.Application.Dtos;
using Qurl;

namespace WorkBoard.Application.Queries.BoardColumns
{
    public class BoardColumnDtoFilter
    {
        public FilterProperty<int> Id { get; set; }
        public FilterProperty<int> BoardId { get; set; }
        public FilterProperty<string> Title { get; set; }
        public FilterProperty<int> Order { get; set; }
        public FilterProperty<string> Description { get; set; }
        public FilterProperty<bool> Active { get; set; }
    }
}
