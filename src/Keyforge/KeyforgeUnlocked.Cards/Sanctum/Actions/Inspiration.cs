using KeyforgeUnlocked.Cards.Attributes;
using KeyforgeUnlocked.Effects.Choices;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Sanctum.Actions
{
  [CardInfo("Inspiration", Rarity.Common,
    "Play: Ready and use a friendly creature.",
    "\"The Sanctum gives meaning to my life.\" - Duma the Martyr")]
  [ExpansionSet(Expansion.CotA, 220)]
  public sealed class Inspiration : ActionCard
  {
    static readonly Callback PlayAbility = (s, _, _) =>
    {
      s.AddEffect(new TargetSingleCreature(
          (s, _, _) => s.AddEffect(new TargetSingleCreature(Delegates.ReadyAndUse(), Target.Own))));
    };

    public Inspiration() : this(House.Sanctum)
    {
    }

    public Inspiration(House house) : base(house, playAbility: PlayAbility)
    {
    }
  }
}