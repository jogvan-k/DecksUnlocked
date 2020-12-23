using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.CreatureCards;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Untamed.Creatures
{
  public class DewFaerie : CreatureCard
  {
    const int Power = 2;
    const int Armor = 0;
    static readonly CreatureType[] creatureTypes = {CreatureType.Faerie};
    static readonly Callback reapAbility = (s, _) => s.GainAember(s.playerTurn);

    public DewFaerie() : this(House.Untamed)
    {
    }
    
    public DewFaerie(House house) : base(house, Power, Armor, creatureTypes, reapAbility: reapAbility)
    {
    }
  }
}