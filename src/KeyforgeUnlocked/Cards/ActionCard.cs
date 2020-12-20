using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards
{
  public abstract class ActionCard : Card
  {
    protected ActionCard(House house, Callback playAbility = null) : base(house, CardType.Action, playAbility)
    {
    }
  }
}