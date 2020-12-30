using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.Effects.Choices;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Brobnar.Actions
{
  public sealed class BloodMoney : ActionCard
  {
    static readonly Callback PlayAbility = 
      (s, _, _) => s.AddEffect(new TargetSingleCreature((s, t, _) => s.AddAemberToCreature(t, 2), Targets.Opponens));

    public BloodMoney() : this(House.Brobnar)
    {
    }

    public BloodMoney(House house) : base(house, playAbility: PlayAbility)
    {
    }
  }
}