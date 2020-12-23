using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.CreatureCards;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Brobnar.Creatures
{
  public class Headhunter : CreatureCard
  {
    const int Power = 5;
    const int Armor = 0;
    static readonly CreatureType[] CreatureTypes = {CreatureType.Giant};
    static readonly Callback FightAbility = (s, _) => s.GainAember(s.playerTurn);

    public Headhunter() : this(House.Brobnar)
    {
    }

    public Headhunter(House house) : base(house, Power, Armor, CreatureTypes, fightAbility: FightAbility)
    {
    }
  }
}