using Hco.Base.Domain;
using Hco.Base.Domain.Exceptions;

namespace WorkBoard.Domain.Model
{
    public class Board : AggregateRoot<int>
    {
        private string _title;
        private string _description;
        private IBoardState _state;

        protected Board()
        {
            _state = new OpenState(this);
        }

        public Board(string title, string description)
        {
            _title = title;
            _description = description;
            _state = new OpenState(this);
        }

        public void Close()
        {
            _state.Close();
        }

        public void ReOpen()
        {
            _state.Open();
        }

        public bool IsOpen()
        {
            return _state.IsOpen();
        }

        internal void SetState(IBoardState state)
        {
            _state = state;
        }
    }
}
