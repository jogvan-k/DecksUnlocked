using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Shadows.Creatures
{
  [CardName("Noddy the Thief")]
  public sealed class NoddyTheThief : CreatureCard
  {
    const int Power = 2;
    const int Armor = 0;
    static readonly CreatureType[] CreatureTypes = {CreatureType.Elf, CreatureType.Thief};
    static readonly Keyword[] Keywords = {Keyword.Elusive};
    static readonly Callback CreatureAbility = (s, _) => s.StealAember(s.playerTurn);

    public NoddyTheThief() : this(House.Shadows)
    {
    }

    public NoddyTheThief(House house) : base(
      house, Power, Armor, CreatureTypes, Keywords, creatureAbility: CreatureAbility)
    {
    }
  }
}