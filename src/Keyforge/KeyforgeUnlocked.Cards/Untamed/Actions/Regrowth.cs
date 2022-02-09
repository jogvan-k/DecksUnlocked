using KeyforgeUnlocked.Cards.Attributes;
using KeyforgeUnlocked.Effects.Choices;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Untamed.Actions
{
  [CardInfo("Regrowth", Rarity.Common,
    "Play: Return a creature from your discard pile to your hand.",
    "\"Deep in the heart of every bear, one can find... another bear.\" -Dr. Escotera")]
  [ExpansionSet(Expansion.CotA, 332)]
  [ExpansionSet(Expansion.AoA, 329)]
  [ExpansionSet(Expansion.WC, 362)]
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