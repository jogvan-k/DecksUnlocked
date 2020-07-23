using System;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Creatures;
using UnlockedCore.Actions;
using UnlockedCore.States;

namespace KeyforgeUnlocked.Cards.CreatureCards
{
  public sealed class LogosCreatureCard : CreatureCard
  {
    const int InitialPower = 3;
    const int InitialArmor = 0;

    public LogosCreatureCard(House house = House.Logos) : base(
      house, InitialPower,
      InitialArmor)
    {
    }
  }
}