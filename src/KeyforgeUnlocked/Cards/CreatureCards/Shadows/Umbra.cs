using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.CreatureCards.Shadows
{
  public sealed class Umbra : CreatureCard
  {
    const string name = "Umbra";
    const int power = 2;
    const int armor = 0;
    static Keyword[] keywords = {Keyword.Skirmish};
    static Delegates.Callback fightAbility = state => state.Steal(1);

    public Umbra() : this(House.Shadows)
    {
    }

    public Umbra(House house) : base(
      name, house, power,
      armor, keywords, fightAbility)
    {
    }
  }
}