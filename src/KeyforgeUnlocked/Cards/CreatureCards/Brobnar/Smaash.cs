using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.CreatureCards.Brobnar
{
  public sealed class Smaash : CreatureCard
  {
    const int power = 5;
    const int armor = 0;
    static CreatureType[] creatureTypes = {CreatureType.Giant};
    static Delegates.Callback playAbility = (s, id) => { };

    public Smaash() : this(House.Brobnar)
    {
    }

    public Smaash(House house) : base(
      house, power, armor, creatureTypes)
    {
    }
  }
}