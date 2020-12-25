using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Brobnar.Actions
{
  public sealed class BloodMoney : ActionCard
  {
    static readonly Callback PlayAbility = 
      (s, _) => s.AddEffect(new TargetSingleCreature((s, t) => s.AddAemberToCreature(t.Id, 2), Delegates.EnemiesOf(s.playerTurn)));

    public BloodMoney() : this(House.Brobnar)
    {
    }

    public BloodMoney(House house) : base(house, playAbility: PlayAbility)
    {
    }
  }
}