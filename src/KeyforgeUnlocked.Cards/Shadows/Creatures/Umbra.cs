using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Shadows.Creatures
{
  public sealed class Umbra : CreatureCard
  {
    const int Power = 2;
    const int Armor = 0;
    static readonly CreatureType[] CreatureTypes = {CreatureType.Elf, CreatureType.Thief};
    static readonly Keyword[] Keywords = {Keyword.Skirmish};
    static readonly Callback FightAbility = (s, _) => s.StealAember(s.playerTurn);

    public Umbra() : this(House.Shadows)
    {
    }

    public Umbra(House house) : base(
      house, Power,
      Armor, CreatureTypes, Keywords, fightAbility: FightAbility)
    {
    }
  }
}