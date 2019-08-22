using System;
using WorkBoard.Application.Dtos;
using Qurl;

namespace WorkBoard.Application.Queries.Boards
{
    public class BoardDtoFilter
    {
        public FilterProperty<int> Id { get; set; }
        public FilterProperty<string> Title { get; set; }
        public FilterProperty<string> Description { get; set; }
        public FilterProperty<BoardState> State { get; set; }
    }
}
