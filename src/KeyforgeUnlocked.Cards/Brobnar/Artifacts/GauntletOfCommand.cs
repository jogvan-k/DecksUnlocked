using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Effects.Choices;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Brobnar.Artifacts
{
  [CardName("Gauntlet of Command")]
  public sealed class GauntletOfCommand : ArtifactCard
  {
    static readonly Trait[] traits = {Trait.Item};
    static readonly Callback ActionAbility =
      (s, _, _) => s.AddEffect(new TargetSingleCreature(Delegates.ReadyAndUse(UseCreature.Fight), Targets.Own));

    public GauntletOfCommand() : this(House.Brobnar)
    {
    }

    public GauntletOfCommand(House house) : base(house, traits, actionAbility: ActionAbility)
    {
    }
  }
}