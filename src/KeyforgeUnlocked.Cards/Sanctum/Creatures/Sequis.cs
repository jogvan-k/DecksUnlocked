using KeyforgeUnlocked.Cards.Attributes;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Sanctum.Creatures
{
  [CardInfo("Sequis", Rarity.Common,
    "Reap: Capture 1 æmber.",
    "\"I follow the Æmber light of the Sanctum, the light of truth and hope. What is it you follow?\"")]
  [ExpansionSet(Expansion.CotA, 257)]
  public sealed class Sequis : CreatureCard
  {
    const int power = 4;
    const int armor = 2;
    static readonly Trait[] Types = {Trait.Human, Trait.Knight};
    static readonly Keyword[] Keywords = { };
    static readonly Callback ReapAbility = (s, i, _) => { s.CaptureAember(i); };

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