using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Shadows.Creatures
{
  public sealed class BadPenny : CreatureCard
  {
    const int Power = 1;
    const int Armor = 0;
    static readonly Trait[] Traits = {Trait.Human, Trait.Thief};
    static readonly Callback DestroyedAbility = (s, id, _) => s.ReturnFromDiscard(id);

    public BadPenny() : this(House.Shadows)
    {
    }

    public BadPenny(House house) : base(house, Power, Armor, Traits, destroyedAbility: DestroyedAbility)
    {
    }
  }
}