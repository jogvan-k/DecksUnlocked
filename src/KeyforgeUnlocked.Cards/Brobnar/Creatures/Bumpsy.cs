using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.CreatureCards;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Brobnar.Creatures
{
  [CardName("Bumpsy")]
  public class Bumpsy : CreatureCard
  {
    const int Power = 5;
    const int Armor = 0;

    static readonly CreatureType[] CreatureTypes =
    {
      CreatureType.Giant
    };

    static readonly Callback PlayAbility = (s, _) => s.LoseAember(s.playerTurn.Other());

    public Bumpsy() : this(House.Brobnar)
    {
    }

    public Bumpsy(House house) : base(house, Power, Armor, CreatureTypes, playAbility: PlayAbility)
    {
    }
  }
}