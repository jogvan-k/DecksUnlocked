using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.CreatureCards;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Shadows.Creatures
{
  public sealed class BadPenny : CreatureCard
  {
    const int power = 1;
    const int armor = 0;
    static CreatureType[] creatureTypes = {CreatureType.Human, CreatureType.Thief};
    static Callback destroyedAbility = (s, id) => { s.ReturnFromDiscard(id); };

    public static string SpecialName = "Bad Penny";

    public BadPenny() : this(House.Shadows)
    {
    }

    public BadPenny(House house) : base(house, power, armor, creatureTypes, destroyedAbility: destroyedAbility)
    {
    }
  }
}