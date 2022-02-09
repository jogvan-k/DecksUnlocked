using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Cards.Attributes;
using KeyforgeUnlocked.Effects.Choices;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Brobnar.Artifacts
{
  [CardInfo("Gauntlet of Command", Rarity.Common,
    "Action: Ready and fight with a friendly creature.",
    "\"I said 'take me to your leader' and got a fist to the face.\" -Captain ValJericho")]
  [ExpansionSet(Expansion.CotA, 22)]
  [ExpansionSet(Expansion.AoA, 10)]
  public sealed class GauntletOfCommand : ArtifactCard
  {
    static readonly Trait[] traits = {Trait.Item};
    static readonly Callback ActionAbility =
      (s, _, _) => s.AddEffect(new TargetSingleCreature(Delegates.ReadyAndUse(UseCreature.Fight), Target.Own));

    public GauntletOfCommand() : this(House.Brobnar)
    {
    }

    public GauntletOfCommand(House house) : base(house, traits, actionAbility: ActionAbility)
    {
    }
  }
}