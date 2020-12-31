using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;
using KeyforgeUnlocked.Types.Events;

namespace KeyforgeUnlocked.Cards.Brobnar.Creatures
{
  public class BilgumAvalanche : CreatureCard
  {
    const int Power = 5;
    const int Armor = 0;

    static readonly Trait[] Traits =
    {
      Trait.Giant
    };

    static readonly Callback PlayAbility = (s, i, p) =>
    {
      s.Events.SubscribeUntilLeavesPlay(
        i,
        EventType.KeyForged,
        (s, _, p1) =>
        {
          if (p1 == p)
            s.AddEffect(new TargetAllCreatures((s, i, _) => s.DamageCreature(i, 2), Delegates.EnemiesOf(p)));
        });
    };

    public BilgumAvalanche() : this(House.Brobnar)
    {
    }

    public BilgumAvalanche(House house) : base(house, Power, Armor, Traits, playAbility: PlayAbility)
    {
    }
  }
}