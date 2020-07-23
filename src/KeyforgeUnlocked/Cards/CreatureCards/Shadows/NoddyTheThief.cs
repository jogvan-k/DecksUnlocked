using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.CreatureCards.Shadows
{
  public sealed class NoddyTheThief : CreatureCard
  {
    const int power = 2;
    const int armor = 0;
    static Keyword[] keywords = {Keyword.Elusive};
    static Delegates.Callback creatureAbility = s => s.Steal(1);

    public static string SpecialName = "Noddy the Thief";

    public NoddyTheThief() : this(House.Shadows)
    {
    }

    public NoddyTheThief(House house) : base(house, power, armor, keywords, creatureAbility: creatureAbility)
    {
    }
  }
}