using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using static KeyforgeUnlocked.Types.Delegates;

namespace KeyforgeUnlocked.Cards.CreatureCards.Brobnar
{
  public sealed class Wardrummer : CreatureCard
  {
    const int power = 3;
    const int armor = 0;
    static readonly CreatureType[] _types = {CreatureType.Goblin};

    static readonly Callback PlayAbility = (s, i) =>
    {
      var effect = new TargetAllCreatures(
        ReturnCreatureToHand,
        AlliesOf(i).And(OfHouse(House.Brobnar).And(Not(i))));
      s.Effects.Enqueue(effect);
    };

    public Wardrummer() : this(House.Brobnar)
    {
    }

    public Wardrummer(House house) : base(house, power, armor, _types, playAbility: PlayAbility)
    {
    }
  }
}