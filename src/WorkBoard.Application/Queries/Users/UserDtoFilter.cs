using System;
using WorkBoard.Application.Dtos;
using Qurl;

namespace WorkBoard.Application.Queries.Users
{
    public class UserDtoFilter
    {
        public FilterProperty<int> Id { get; set; }
        public FilterProperty<string> Name { get; set; }
        public FilterProperty<string> LastName { get; set; }
        public FilterProperty<string> Username { get; set; }
        public FilterProperty<string> Password { get; set; }
        public FilterProperty<string> Email { get; set; }
        public FilterProperty<bool> Veryfied { get; set; }
    }
}
