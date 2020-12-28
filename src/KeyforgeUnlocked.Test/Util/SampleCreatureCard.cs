using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlockedTest.Util
{
  public sealed class SampleCreatureCard : Card, ICreatureCard
  {
    public int CardPower { get; }
    public int CardArmor { get; }
    public Keyword[] CardKeywords { get; }
    public CreatureType[] CardTypes { get; }
    public Callback CardFightAbility { get; }
    public Callback CardBeforeFightAbility { get; }
    public Callback CardAfterKillAbility { get; }
    public Callback CardCreatureAbility { get; }
    public Callback CardReapAbility { get; }
    public Callback CardDestroyedAbility { get; }

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
        pips,
        playAbility,
        id: id)
    {
      CardPower = power;
      CardArmor = armor;
      CardTypes = types ?? new CreatureType[0];
      CardKeywords = keywords ?? new Keyword[0];
      CardBeforeFightAbility = beforeFightAbility;
      CardFightAbility = fightAbility;
      CardAfterKillAbility = afterKillAbility;
      CardCreatureAbility = creatureAbility;
      CardReapAbility = reapAbility;
      CardDestroyedAbility = destroyedAbility;
    }

    public Creature InsantiateCreature()
    {
      new Creature(this);
    }
  }
}