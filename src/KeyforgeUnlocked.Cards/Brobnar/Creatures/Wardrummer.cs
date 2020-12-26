using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;
using static KeyforgeUnlocked.Types.Delegates;

namespace KeyforgeUnlocked.Cards.Brobnar.Creatures
{
  public sealed class Wardrummer : CreatureCard
  {
    const int Power = 3;
    const int Armor = 0;
    static readonly CreatureType[] Types = {CreatureType.Goblin};

    static readonly Callback PlayAbility = (s, i) =>
    {
      var effect = new TargetAllCreatures(
        ReturnTargetToHand,
        AlliesOf(i).And(OfHouse(House.Brobnar).And(Not(i))));
      s.AddEffect(effect);
    };

    public Wardrummer() : this(House.Brobnar)
    {
    }

    public Wardrummer(House house) : base(house, Power, Armor, Types, playAbility: PlayAbility)
    {
    }
  }
}