using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.CreatureCards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Shadows.Creatures
{
  public sealed class Umbra : CreatureCard
  {
    const int power = 2;
    const int armor = 0;
    static CreatureType[] creatureTypes = {CreatureType.Elf, CreatureType.Thief};
    static Keyword[] keywords = {Keyword.Skirmish};
    static Callback fightAbility = (s, id) => s.StealAember(s.playerTurn);

    public Umbra() : this(House.Shadows)
    {
    }

    public Umbra(House house) : base(
      house, power,
      armor, creatureTypes, keywords, fightAbility: fightAbility)
    {
    }
  }
}