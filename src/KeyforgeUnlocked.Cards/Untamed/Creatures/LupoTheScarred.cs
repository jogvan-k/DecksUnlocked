using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.CreatureCards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Untamed.Creatures
{
  [CardName("Lupo the Scarred")]
  public class LupoTheScarred : CreatureCard
  {
    const int Power = 6;
    const int Armor = 0;

    static readonly CreatureType[] CreatureTypes =
    {
      CreatureType.Beast
    };

    static readonly Keyword[] Keywords =
    {
      Keyword.Skirmish
    };

    static readonly Callback PlayAbility = (s, _) =>
    {
      var effect = new TargetSingleCreature(
        (s, t) => s.DamageCreature(t, 2), Delegates.EnemiesOf(s.playerTurn));
      s.AddEffect(effect);
    };

    public LupoTheScarred() : this(House.Untamed)
    {
    }

    public LupoTheScarred(House house) : base(house, Power, Armor, CreatureTypes, keywords: Keywords, playAbility: PlayAbility)
    {
    }
  }
}