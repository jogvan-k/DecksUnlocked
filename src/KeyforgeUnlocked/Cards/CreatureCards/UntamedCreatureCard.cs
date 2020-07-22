using System;

namespace KeyforgeUnlocked.Cards.CreatureCards
{
  public sealed class UntamedCreatureCard : CreatureCard
  {
    const int InitialPower = 2;
    const int InitialArmor = 0;

    public UntamedCreatureCard(House house = House.Untamed) : base(
      "UntamedCreature", house, InitialPower, InitialArmor, Array.Empty<Keyword>())
    {
    }
  }
}