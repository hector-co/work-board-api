using Hco.Base.Domain.Exceptions;

namespace WorkBoard.Domain.Model
{
    public class ClosedState : IBoardState
    {
        private readonly Board _board;

        internal ClosedState(Board board)
        {
            _board = board;
        }

        public void Close()
        {
            throw new DomainException("Board is already closed");
        }

        public bool IsOpen()
        {
            return false;
        }

        public void Open()
        {
            _board.SetState(new OpenState(_board));
        }
    }
}
