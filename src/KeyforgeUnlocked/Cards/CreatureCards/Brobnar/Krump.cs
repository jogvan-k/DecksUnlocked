using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using UnlockedCore.States;

namespace KeyforgeUnlocked.Cards.CreatureCards.Brobnar
{
  public sealed class Krump : CreatureCard
  {
    const int power = 6;
    const int armor = 0;
    static readonly CreatureType[] types = {CreatureType.Giant};

    static readonly Callback afterKillAbility = (s, i) =>
    {
      s.FindCreature(i, out var controllingPlayer);
      s.LoseAember(controllingPlayer.Other());
    };

    public Krump(House house = House.Brobnar) : base(house, power, armor, types, afterKillAbility: afterKillAbility)
    {
    }
  }
}