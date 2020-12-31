using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Brobnar.Artifacts
{
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