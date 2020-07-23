using System;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlockedTest.Util
{
  public sealed class SampleCreatureCard : CreatureCard
  {
    public SampleCreatureCard(int power = 1,
      int armor = 0,
      House house = House.Undefined,
      Keyword[] keywords = null,
      Delegates.Callback fightAbility = null,
      Delegates.Callback creatureAbility = null)
      : base(
        house,
        power,
        armor,
        keywords,
        fightAbility,
        creatureAbility)
    {
    }
  }
}