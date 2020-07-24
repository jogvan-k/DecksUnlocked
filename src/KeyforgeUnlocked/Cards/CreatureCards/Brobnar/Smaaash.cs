using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.CreatureCards.Brobnar
{
  public sealed class Smaaash : CreatureCard
  {
    const int power = 5;
    const int armor = 0;
    static CreatureType[] creatureTypes = {CreatureType.Giant};
    static Callback playAbility = (s, id) =>
    {
      s.Effects.Push(new TargetCreature(Delegates.StunCreature, Delegates.All));
    };

    public Smaaash() : this(House.Brobnar)
    {
    }

    public Smaaash(House house) : base(house, power, armor, creatureTypes, playAbility: playAbility)
    {
    }
  }
}