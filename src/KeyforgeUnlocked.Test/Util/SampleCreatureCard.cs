using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.Creatures;
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
      Pip[] pips = null,
      Callback playAbility = null,
      Callback beforeFightAbility = null,
      Callback fightAbility = null,
      Callback afterKillAbility = null,
      Callback creatureAbility = null,
      Callback reapAbility = null,
      Callback destroyedAbility = null,
      string id = null)
      : base(
        house,
        power,
        armor,
        types,
        keywords,
        pips,
        playAbility: playAbility,
        beforeFightAbility: beforeFightAbility,
        fightAbility: fightAbility,
        afterKillAbility: afterKillAbility,
        creatureAbility: creatureAbility,
        reapAbility: reapAbility,
        destroyedAbility: destroyedAbility,
        id: id)
    {
    }
  }
}