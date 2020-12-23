using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.CreatureCards
{
  public abstract class CreatureCard : Card
  {
    public readonly int CardPower;
    public readonly int CardArmor;
    public readonly Keyword[] CardKeywords;

    public readonly CreatureType[] CardTypes;
    public readonly Callback CardFightAbility;
    public readonly Callback CardBeforeFightAbility;
    public readonly Callback CardAfterKillAbility;
    public readonly Callback CardCreatureAbility;
    public readonly Callback CardReapAbility;
    public readonly Callback CardDestroyedAbility;

    protected CreatureCard(
      House house,
      int power,
      int armor,
      CreatureType[] types = null,
      Keyword[] keywords = null,
      Callback playAbility = null,
      Callback beforeFightAbility = null,
      Callback fightAbility = null,
      Callback afterKillAbility = null,
      Callback creatureAbility = null,
      Callback reapAbility = null,
      Callback destroyedAbility = null,
      string id = null) : base(house, CardType.Creature, playAbility, id)
    {
      CardPower = power;
      CardArmor = armor;
      CardBeforeFightAbility = beforeFightAbility;
      CardKeywords = keywords ?? new Keyword[0];
      CardTypes = types ?? new CreatureType[0];
      CardFightAbility = fightAbility;
      CardAfterKillAbility = afterKillAbility;
      CardCreatureAbility = creatureAbility;
      CardReapAbility = reapAbility;
      CardDestroyedAbility = destroyedAbility;
    }

    public Creature InsantiateCreature()
    {
      return new (this);
    }
  }
}