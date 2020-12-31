using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Logos.Creatures
{
  public class DocBookton : CreatureCard
  {
    const int Power = 5;
    const int Armor = 0;

    static readonly Trait[] Traits =
    {
      Trait.Human, Trait.Scientist
    };

    static readonly Callback ReapAbility = (s, _, p) => s.Draw(p);

    public DocBookton() : this(House.Brobnar)
    {
    }

    public DocBookton(House house) : base(house, Power, Armor, Traits, reapAbility: ReapAbility)
    {
    }
  }
}