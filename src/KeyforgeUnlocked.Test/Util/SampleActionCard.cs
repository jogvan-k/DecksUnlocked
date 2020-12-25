using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlockedTest.Util
{
  public sealed class SampleActionCard : ActionCard
  {
    public SampleActionCard(
      House house = House.Undefined,
      Pip[] pips = null,
      Callback playAbility = null) : base(house, pips, playAbility)
    {
    }
  }
}