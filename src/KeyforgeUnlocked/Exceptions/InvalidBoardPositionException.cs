namespace KeyforgeUnlocked.Exceptions
{
  public class InvalidBoardPositionException : KeyforgeUnlockedException
  {
    public int boardPosition { get; }

    public InvalidBoardPositionException(State state, int boardPosition) : base(state)
    {
      this.boardPosition = boardPosition;
    }
  }
}