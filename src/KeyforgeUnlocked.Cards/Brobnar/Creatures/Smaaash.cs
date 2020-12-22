using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.CreatureCards;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Brobnar.Creatures
{
  public sealed class Smaaash : CreatureCard
  {
    const int Power = 5;
    const int Armor = 0;
    static readonly CreatureType[] CreatureTypes = {CreatureType.Giant};
    static readonly Callback PlayAbility = (s, _) =>
    {
      s.Effects.Push(new TargetSingleCreature(Delegates.StunCreature, Delegates.All));
    };

    public Smaaash() : this(House.Brobnar)
    {
    }

    public Smaaash(House house) : base(house, Power, Armor, CreatureTypes, playAbility: PlayAbility)
    {
    }
  }
}