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
      Callback playAbility = null,
      Callback fightAbility = null,
      Callback creatureAbility = null,
      Callback reapAbility = null,
      Callback destroyedAbility = null)
      : base(
        house,
        power,
        armor,
        types,
        keywords,
        playAbility,
        fightAbility,
        creatureAbility,
        reapAbility,
        destroyedAbility)
    {
    }
  }
}