using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Exceptions
{
  public sealed class CardNotPresentException : KeyforgeUnlockedException
  {
    public string CardId;
    public CardNotPresentException(IState state, string cardId) : base(state)
    {
      CardId = cardId;
    }
  }
}