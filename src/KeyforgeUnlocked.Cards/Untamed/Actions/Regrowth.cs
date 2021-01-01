using KeyforgeUnlocked.Effects.Choices;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Untamed.Actions
{
  public sealed class Regrowth : ActionCard
  {
    static readonly Pip[] Pips = {Pip.Aember};

    static readonly Callback PlayAbility =
      (s, _, _) => s.AddEffect(
        new TargetSingleDiscardedCard((s, t, _) => s.ReturnFromDiscard(t), Target.Own, Delegates.IsCreatureCard()));

    public Regrowth() : this(House.Untamed)
    {
    }

    public Regrowth(House house) : base(house, pips: Pips, PlayAbility)
    {
    }
  }
}