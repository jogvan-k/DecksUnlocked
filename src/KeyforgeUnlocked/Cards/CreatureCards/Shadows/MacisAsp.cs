using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.CreatureCards.Shadows
{
  public sealed class MacisAsp : CreatureCard
  {
    const int power = 3;
    const int armor = 0;
    static readonly CreatureType[] _creatureTypes = {CreatureType.Beast};
    static readonly Keyword[] _keywords = {Keyword.Skirmish, Keyword.Poison};

    public static string SpecialName = "Macis Asp";

    public MacisAsp() : this(House.Shadows)
    {
    }

    public MacisAsp(House house) : base(house, power, armor, _creatureTypes, _keywords)
    {
    }
  }
}