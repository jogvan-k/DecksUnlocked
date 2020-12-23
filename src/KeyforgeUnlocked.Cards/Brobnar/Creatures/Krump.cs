using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.CreatureCards;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Brobnar.Creatures
{
  public sealed class Krump : CreatureCard
  {
    const int Power = 6;
    const int Armor = 0;
    static readonly CreatureType[] Types = {CreatureType.Giant};

    static readonly Callback AfterKillAbility = (s, i) =>
    {
      s.FindCreature(i, out var controllingPlayer, out _);
      s.LoseAember(controllingPlayer.Other());
    };

    public Krump() : this(House.Brobnar)
    {
    }

    public Krump(House house) : base(house, Power, Armor, Types, afterKillAbility: AfterKillAbility)
    {
    }
  }
}