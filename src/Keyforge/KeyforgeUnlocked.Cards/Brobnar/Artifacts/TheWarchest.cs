using KeyforgeUnlocked.Cards.Attributes;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Brobnar.Artifacts
{
  [CardInfo("The Warchest", Rarity.Uncommon,
    "Action: Gain 1 æmber for each enemy creature that was destroyed in a fight this turn.",
    "It doesn't matter what the treasure is, only how it was won.")]
  [ExpansionSet(Expansion.CotA, 27)]
  [ExpansionSet(Expansion.AoA, 32)]
  public sealed class TheWarchest : ArtifactCard
  {
    static readonly Trait[] traits = {Trait.Item};

    static readonly Callback ActionAbility =
      (s, _, _) => s.GainAember(s.HistoricData.EnemiesDestroyedInAFightThisTurn);

    public TheWarchest() : this(House.Brobnar)
    {
    }

    public TheWarchest(House house) : base(house, traits, actionAbility: ActionAbility)
    {
    }
  }
}