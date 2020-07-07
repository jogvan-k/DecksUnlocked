namespace KeyforgeUnlocked.Exceptions
{
  public class CardNotPresentException : KeyforgeUnlockedException
  {
    public CardNotPresentException(State state) : base(state)
    {
    }
  }
}