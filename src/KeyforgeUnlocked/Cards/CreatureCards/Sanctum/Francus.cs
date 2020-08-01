using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.CreatureCards.Sanctum
{
  public sealed class Francus : CreatureCard
  {
    const int power = 6;
    const int armor = 1;
    static readonly CreatureType[] types = {CreatureType.Knight, CreatureType.Spirit};
    static readonly Callback afterKillAbility = (s, i) => { s.CaptureAember(i); };

    public Francus() : this(House.Sanctum)
    {
    }

    public Francus(House house) : base(house, power, armor, types, afterKillAbility: afterKillAbility)
    {
    }
  }
}