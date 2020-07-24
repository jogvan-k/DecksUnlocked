using System;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlockedTest.Util
{
  public sealed class SampleCreatureCard : CreatureCard
  {
    public SampleCreatureCard(
      House house = House.Undefined,
      int power = 1,
      int armor = 0,
      CreatureType[] types = null,
      Keyword[] keywords = null,
      Delegates.Callback fightAbility = null,
      Delegates.Callback creatureAbility = null)
      : base(
        house,
        power,
        armor,
        types,
        keywords,
        fightAbility,
        creatureAbility)
    {
    }
  }
}