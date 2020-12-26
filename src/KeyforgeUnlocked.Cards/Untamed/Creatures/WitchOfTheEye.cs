using KeyforgeUnlocked.Cards.CreatureCards;
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

    static readonly CreatureType[] CreatureTypes =
    {
      CreatureType.Human,
      CreatureType.Witch
    };

    static readonly Callback ReapAbility = (s, _) =>
    {
      var effect = new TargetSingleDiscardedCard(
        (s, t) => s.ReturnFromDiscard(t), Delegates.BelongingTo(s.playerTurn)); // TODO
      s.AddEffect(effect);
    };

    public WitchOfTheEye() : this(House.Untamed)
    {
    }

    public WitchOfTheEye(House house) : base(house, Power, Armor, CreatureTypes, reapAbility: ReapAbility)
    {
    }
  }
}