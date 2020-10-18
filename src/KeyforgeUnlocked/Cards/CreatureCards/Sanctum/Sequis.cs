using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.CreatureCards.Sanctum
{
  public sealed class Sequis : CreatureCard
  {
    const int power = 4;
    const int armor = 2;
    static readonly CreatureType[] types = {CreatureType.Human, CreatureType.Knight};
    static readonly Keyword[] keywords = { };
    static readonly Callback reapAbility = (s, i) => { s.CaptureAember(i);};

    public Sequis() : this(House.Sanctum)
    {
    }

    public Sequis(House house) : base(
      house, power, armor, types, keywords, null,
      null, null, null, reapAbility, null)
    {
    }
  }
}