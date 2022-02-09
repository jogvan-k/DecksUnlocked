using KeyforgeUnlocked.Cards.Attributes;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Brobnar.Creatures
{
  [CardInfo("Wardrummer", Rarity.Common,
    "Play: Return each other friendly Brobnar creature to your hand")]
  [ExpansionSet(Expansion.CotA, 49)]
  public sealed class Wardrummer : CreatureCard
  {
    const int Power = 3;
    const int Armor = 0;
    static readonly Trait[] Types = {Trait.Goblin};

    static readonly Callback PlayAbility = (s, i, p) =>
    {
      var effect = new TargetAllCreatures(Delegates.ReturnTargetToHand, Delegates.AlliesOf(p).And(Delegates.OfHouse(House.Brobnar).And(Delegates.Not(i))));
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