using KeyforgeUnlocked.Effects.Choices;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Untamed.Creatures
{
  [CardName("Witch of the Eye")]
  public class WitchOfTheEye : CreatureCard
  {
    const int Power = 3;
    const int Armor = 0;

    static readonly Trait[] Traits =
    {
      Trait.Human,
      Trait.Witch
    };

    static readonly Callback ReapAbility = (s, _, _) =>
    {
      var effect = new TargetSingleDiscardedCard(
        (s, t, _) => s.ReturnFromDiscard(t), Targets.Own);
      s.AddEffect(effect);
    };

    public WitchOfTheEye() : this(House.Untamed)
    {
    }

    public WitchOfTheEye(House house) : base(house, Power, Armor, Traits, reapAbility: ReapAbility)
    {
    }
  }
}