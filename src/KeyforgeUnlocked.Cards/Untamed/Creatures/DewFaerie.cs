using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Untamed.Creatures
{
  public class DewFaerie : CreatureCard
  {
    const int Power = 2;
    const int Armor = 0;
    static readonly Trait[] Traits = {Trait.Faerie};
    static readonly Callback reapAbility = (s, _, _) => s.GainAember();

    public DewFaerie() : this(House.Untamed)
    {
    }
    
    public DewFaerie(House house) : base(house, Power, Armor, Traits, reapAbility: reapAbility)
    {
    }
  }
}