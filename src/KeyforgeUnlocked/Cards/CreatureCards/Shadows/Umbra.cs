using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.CreatureCards.Shadows
{
  public sealed class Umbra : CreatureCard
  {
    const int power = 2;
    const int armor = 0;
    static CreatureType[] creatureTypes = {CreatureType.Elf, CreatureType.Thief};
    static Keyword[] keywords = {Keyword.Skirmish};
    static Delegates.Callback fightAbility = state => state.Steal(1);

    public Umbra() : this(House.Shadows)
    {
    }

    public Umbra(House house) : base(
      house, power,
      armor, creatureTypes, keywords, fightAbility)
    {
    }
  }
}