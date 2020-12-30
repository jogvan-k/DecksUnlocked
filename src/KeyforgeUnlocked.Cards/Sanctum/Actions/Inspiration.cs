using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.Effects.Choices;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Sanctum.Actions
{
  public sealed class Inspiration : ActionCard
  {
    static readonly Callback PlayAbility = (s, _, _) =>
    {
      s.AddEffect(new TargetSingleCreature(
          (s, _, _) => s.AddEffect(new TargetSingleCreature(Delegates.ReadyAndUse(), Targets.Own))));
    };

    public Inspiration() : this(House.Sanctum)
    {
    }

    public Inspiration(House house) : base(house, playAbility: PlayAbility)
    {
    }
  }
}