using System;
using WorkBoard.Application.Dtos;
using Qurl;

namespace WorkBoard.Application.Queries.Cards
{
    public class CardDtoFilter
    {
        public FilterProperty<int> Id { get; set; }
        public FilterProperty<int> BoardId { get; set; }
        public FilterProperty<int> ColumnId { get; set; }
        public FilterProperty<string> Title { get; set; }
        public FilterProperty<string> Description { get; set; }
        public FilterProperty<int> Color { get; set; }
        public FilterProperty<CardPriority> Priority { get; set; }
        public FilterProperty<float> EstimatedPoints { get; set; }
        public FilterProperty<float> ConsumedPoints { get; set; }
        public FilterProperty<bool> Done { get; set; }
        public FilterProperty<int> Order { get; set; }
    }
}
