using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.ActionCards.Sanctum
{
  public sealed class Inspiration : ActionCard
  {
    static Callback PlayAbility = (s, i) =>
    {
      s.Effects.Push(
        new TargetSingleCreature((s, c) => s.Effects.Push(new ReadyAndUse(c)), Delegates.AlliesOf(s.playerTurn)));
    };

    public Inspiration() : this(House.Sanctum)
    {
    }

    public Inspiration(House house) : base(house, PlayAbility)
    {
    }
  }
}