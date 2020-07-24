using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.CreatureCards.Shadows
{
  public sealed class NoddyTheThief : CreatureCard
  {
    const int power = 2;
    const int armor = 0;
    static CreatureType[] creatureTypes = {CreatureType.Elf, CreatureType.Thief};
    static Keyword[] keywords = {Keyword.Elusive};
    static Callback creatureAbility = (s, id) => s.Steal(1);

    public static string SpecialName = "Noddy the Thief";

    public NoddyTheThief() : this(House.Shadows)
    {
    }

    public NoddyTheThief(House house) : base(
      house, power, armor, creatureTypes, keywords, creatureAbility: creatureAbility)
    {
    }
  }
}