using KeyforgeUnlocked.Creatures;
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

    public Francus(House house = House.Sanctum) : base(house, power, armor, types, afterKillAbility: afterKillAbility)
    {
    }
  }
}