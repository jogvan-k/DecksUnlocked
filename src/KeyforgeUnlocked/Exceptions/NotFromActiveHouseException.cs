using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Exceptions
{
  public sealed class NotFromActiveHouseException : KeyforgeUnlockedException
  {
    public Card Card;
    public House House;
    
    public NotFromActiveHouseException(IState state,
      Card card,
      House house) : base(state)
    {
      Card = card;
      House = house;
    }
  }
}