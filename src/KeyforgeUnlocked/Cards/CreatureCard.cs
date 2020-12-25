using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards
{
  public abstract class CreatureCard : Card, ICreatureCard
  {
    public int CardPower  { get; }
    public int CardArmor { get; }
    public Keyword[] CardKeywords { get; }

    public CreatureType[] CardTypes { get; }
    public Callback CardFightAbility { get; }
    public Callback CardBeforeFightAbility { get; }
    public Callback CardAfterKillAbility { get; }
    public Callback CardCreatureAbility { get; }
    public Callback CardReapAbility { get; }
    public Callback CardDestroyedAbility { get; }
    

    protected CreatureCard(
      House house,
      int power,
      int armor,
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
      string id = null) : base(house, pips, playAbility, id)
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