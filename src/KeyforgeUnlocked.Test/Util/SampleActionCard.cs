using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlockedTest.Util
{
  public sealed class SampleActionCard : Card, IActionCard
  {
    public SampleActionCard(
      House house = House.Undefined,
      Pip[] pips = null,
      Callback playAbility = null,
      string id = null) : base(house, pips, playAbility, id)
    {
    }
  }
}