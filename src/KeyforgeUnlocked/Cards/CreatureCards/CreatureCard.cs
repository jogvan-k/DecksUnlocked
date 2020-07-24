using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.CreatureCards
{
  public abstract class CreatureCard : Card
  {
    public int Power { get; }
    public int Armor { get; }
    public Keyword[] Keywords { get; }

    public CreatureType[] Types { get; }
    public Delegates.Callback FightAbility { get; }
    public Delegates.Callback CreatureAbility { get; }

    protected CreatureCard(
      House house,
      int power,
      int armor,
      CreatureType[] types = null,
      Keyword[] keywords = null,
      Delegates.Callback fightAbility = null,
      Delegates.Callback creatureAbility = null) : base(house, CardType.Creature)
    {
      Power = power;
      Armor = armor;
      Keywords = keywords ?? new Keyword[0];
      Types = types ?? new CreatureType[0];
      FightAbility = fightAbility;
      CreatureAbility = creatureAbility;
    }

    public Creature InsantiateCreature()
    {
      return new Creature(this);
    }
  }
}