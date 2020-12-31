using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Brobnar.Creatures
{
  [CardName("Bumpsy")]
  public class Bumpsy : CreatureCard
  {
    const int Power = 5;
    const int Armor = 0;

    static readonly Trait[] Traits =
    {
      Trait.Giant
    };

    static readonly Callback PlayAbility = (s, _, p) => s.LoseAember(p.Other());

    public Bumpsy() : this(House.Brobnar)
    {
    }

    public Bumpsy(House house) : base(house, Power, Armor, Traits, playAbility: PlayAbility)
    {
    }
  }
}