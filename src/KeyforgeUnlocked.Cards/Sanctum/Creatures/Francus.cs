using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.CreatureCards;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Sanctum.Creatures
{
  public sealed class Francus : CreatureCard
  {
    const int Power = 6;
    const int Armor = 1;
    static readonly CreatureType[] Types = {CreatureType.Knight, CreatureType.Spirit};
    static readonly Callback AfterKillAbility = (s, i) => { s.CaptureAember(i); };

    public Francus() : this(House.Sanctum)
    {
    }

    public Francus(House house) : base(house, Power, Armor, Types, afterKillAbility: AfterKillAbility)
    {
    }
  }
}