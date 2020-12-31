using System.Linq;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Untamed.Creatures
{
  public class GiantSloth : CreatureCard
  {
    const int Power = 6;
    const int Armor = 0;

    static readonly Trait[] Traits =
    {
      Trait.Beast
    };

    static readonly Callback CreatureGroups = (s, _, _) => s.GainAember(3);

    static readonly ActionPredicate ActionAllowed = (s, _) =>
      s.HistoricData.CardsDiscardedThisTurn.Any(c => c.House == House.Untamed);
    
    public GiantSloth() : this(House.Untamed)
    {
    }

    public GiantSloth(House house) : base(house, Power, Armor, Traits, creatureAbility: CreatureGroups, actionAllowed: ActionAllowed)
    {
    }
  }
}