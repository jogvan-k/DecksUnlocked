using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Sanctum.Creatures
{
  public sealed class Sequis : CreatureCard
  {
    const int power = 4;
    const int armor = 2;
    static readonly CreatureType[] Types = {CreatureType.Human, CreatureType.Knight};
    static readonly Keyword[] Keywords = { };
    static readonly Callback ReapAbility = (s, i) => { s.CaptureAember(i); };

    public Sequis() : this(House.Sanctum)
    {
    }

    public Sequis(House house) : base(
      house, power, armor, Types, Keywords, null,
      null, null, null, ReapAbility, null)
    {
    }
  }
}