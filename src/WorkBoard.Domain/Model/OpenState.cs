using Hco.Base.Domain.Exceptions;

namespace WorkBoard.Domain.Model
{
    public class OpenState : IBoardState
    {
        private readonly Board _board;

        internal OpenState(Board board)
        {
            _board = board;
        }

        public void Close()
        {
            _board.SetState(new ClosedState(_board));
        }

        public bool IsOpen()
        {
            return true;
        }

        public void Open()
        {
            throw new DomainException("Board is already open");
        }
    }
}
