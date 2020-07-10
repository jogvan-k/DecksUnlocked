using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Exceptions
{
  public sealed class CardNotPresentException : KeyforgeUnlockedException
  {
    public CardNotPresentException(IState state) : base(state)
    {
    }
  }
}