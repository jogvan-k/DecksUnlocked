using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlockedTest.Util
{
  public sealed class SampleActionCard : Card, IActionCard
  {
    public SampleActionCard(House house) : this(house, null) {}
    public SampleActionCard(
      House house = House.Undefined,
      Pip[] pips = null,
      Callback playAbility = null,
      ActionPredicate playAllowed = null,
      string id = null) : base(house, pips, playAbility, playAllowed, id)
    {
    }
  }
}