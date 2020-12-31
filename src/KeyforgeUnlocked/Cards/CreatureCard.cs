using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards
{
  public abstract class CreatureCard : Card, ICreatureCard
  {
    public int CardPower  { get; }
    public int CardArmor { get; }
    public Keyword[] CardKeywords { get; }
    public Trait[] CardTraits { get; }
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
      Trait[] traits = null,
      Keyword[] keywords = null,
      Pip[] pips = null,
      Callback playAbility = null,
      Callback beforeFightAbility = null,
      Callback fightAbility = null,
      Callback afterKillAbility = null,
      Callback creatureAbility = null,
      Callback reapAbility = null,
      Callback destroyedAbility = null) : base(house, pips, playAbility)
    {
      CardPower = power;
      CardArmor = armor;
      CardTraits = traits ?? new Trait[0];
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
      return new (this);
    }
  }
}