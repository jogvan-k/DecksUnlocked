using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Sanctum.Actions
{
  public sealed class Inspiration : ActionCard
  {
    static readonly Callback _playAbility = (s, i) =>
    {
      s.Effects.Push(
        new TargetSingleCreature(
          (s, c) => s.Effects.Push(new ReadyAndUse(c, true)), Delegates.AlliesOf(s.playerTurn)));
    };

    public Inspiration() : this(House.Sanctum)
    {
    }

    public Inspiration(House house) : base(house, _playAbility)
    {
    }
  }
}