namespace WorkBoard.Domain.Model
{
    public interface IBoardState
    {
        void Close();
        void Open();
        bool IsOpen();
    }
}
