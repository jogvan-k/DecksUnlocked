using KeyforgeUnlocked.Effects.Choices;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Untamed.Actions
{
  public sealed class Regrowth : ActionCard
  {
    static readonly Pip[] Pips = {Pip.Aember};

    static readonly Callback PlayAbility =
      (s, _) => s.AddEffect(
        new TargetSingleDiscardedCard((s, t) => s.ReturnFromDiscard(t), Targets.Own, Delegates.IsCreatureCard()));

    public Regrowth() : this(House.Untamed)
    {
    }

    public Regrowth(House house) : base(house, pips: Pips, PlayAbility)
    {
    }
  }
}