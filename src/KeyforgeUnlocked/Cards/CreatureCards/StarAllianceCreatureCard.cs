using System;

namespace KeyforgeUnlocked.Cards.CreatureCards
{
  public sealed class StarAllianceCreatureCard : CreatureCard
  {
    const int InitialPower = 2;
    const int InitialArmor = 2;

    public StarAllianceCreatureCard(House house = House.StarAlliance)
      : base(
        house, InitialPower,
        InitialArmor, Array.Empty<Keyword>(), null)
    {
    }
  }
}