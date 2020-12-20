using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.CreatureCards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Sanctum.Creatures
{
  public sealed class SanctumGuardian : CreatureCard
  {
    const int power = 6;
    const int armor = 1;
    static CreatureType[] creatureTypes = {CreatureType.Knight, CreatureType.Spirit};
    static Keyword[] keywords = {Keyword.Taunt};

    static Callback fightReapAbility = (s, self) =>
    {
      s.Effects.Push(new TargetSingleCreature(Delegates.SwapCreatures(self), Delegates.AlliesOf(self)));
    };

    public static string SpecialName = "Sanctum Guardian";

    public SanctumGuardian() : this(House.Sanctum)
    {
    }

    public SanctumGuardian(House house) : base(
      house, power, armor, creatureTypes, keywords, fightAbility: fightReapAbility, reapAbility: fightReapAbility)
    {
    }
  }
}