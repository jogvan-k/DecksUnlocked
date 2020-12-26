using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.Effects.Choices;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Sanctum.Creatures
{
  public sealed class SanctumGuardian : CreatureCard
  {
    const int power = 6;
    const int armor = 1;
    static readonly CreatureType[] CreatureTypes = {CreatureType.Knight, CreatureType.Spirit};
    static readonly Keyword[] Keywords = {Keyword.Taunt};

    static readonly Callback FightReapAbility = (s, self) =>
    {
      s.AddEffect(new TargetSingleCreature(Delegates.SwapCreatures(self), Delegates.AlliesOf(self)));
    };

    public SanctumGuardian() : this(House.Sanctum)
    {
    }

    public SanctumGuardian(House house) : base(
      house, power, armor, CreatureTypes, Keywords, fightAbility: FightReapAbility, reapAbility: FightReapAbility)
    {
    }
  }
}