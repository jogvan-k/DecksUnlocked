using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Exceptions
{
  public class CardNotPresentException : KeyforgeUnlockedException
  {
    public CardNotPresentException(IState state) : base(state)
    {
    }
  }
}