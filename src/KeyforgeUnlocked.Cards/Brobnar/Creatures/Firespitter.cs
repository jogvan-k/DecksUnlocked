using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Brobnar.Creatures
{
  public class Firespitter : CreatureCard
  {
    const int Power = 5;
    const int Armor = 1;

    static readonly CreatureType[] CreatureTypes =
    {
      CreatureType.Giant
    };

    static readonly Callback BeforeFightAbility = (s, _, _) =>
    {
      var effect = new TargetAllCreatures(
        (s, t, _) => s.DamageCreature(t), Delegates.EnemiesOf(s.PlayerTurn));
      effect.Resolve(s);
    };

    public Firespitter() : this(House.Brobnar)
    {
    }

    public Firespitter(House house) : base(house, Power, Armor, CreatureTypes, beforeFightAbility: BeforeFightAbility)
    {
    }
  }
}