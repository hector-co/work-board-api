using System;
using System.Collections.Generic;
using Hco.Base.Domain;

namespace WorkBoard.Domain.Model
{
    public class User : AggregateRoot<int>
    {
        private string _name;
        private string _lastName;
        private string _username;
        private string _password;
        private string _email;
        private bool _veryfied;

		protected User()
		{
		}
    }
}
