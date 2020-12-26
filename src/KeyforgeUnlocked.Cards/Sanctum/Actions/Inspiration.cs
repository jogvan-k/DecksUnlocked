using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.Effects.Choices;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Sanctum.Actions
{
  public sealed class Inspiration : ActionCard
  {
    static readonly Callback PlayAbility = (s, i) =>
    {
      s.Effects.Push(
        new TargetSingleCreature(
          (s, c) => s.Effects.Push(new ReadyAndUseCreature(c, true)), Targets.Own));
    };

    public Inspiration() : this(House.Sanctum)
    {
    }

    public Inspiration(House house) : base(house, playAbility: PlayAbility)
    {
    }
  }
}