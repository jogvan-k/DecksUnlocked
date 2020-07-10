using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Exceptions
{
  public sealed class InvalidBoardPositionException : KeyforgeUnlockedException
  {
    public int boardPosition { get; }

    public InvalidBoardPositionException(IState state, int boardPosition) : base(state)
    {
      this.boardPosition = boardPosition;
    }
  }
}