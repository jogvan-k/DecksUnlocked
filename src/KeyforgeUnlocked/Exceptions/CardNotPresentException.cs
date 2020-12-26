using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Exceptions
{
  public sealed class CardNotPresentException : KeyforgeUnlockedException
  {
    public IIdentifiable Id;
    public CardNotPresentException(IState state, IIdentifiable id) : base(state)
    {
      Id = id;
    }
  }
}