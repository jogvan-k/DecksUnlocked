using System;
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
    public Delegates.Callback DestroyedAbility { get; }

    protected CreatureCard(
      House house,
      int power,
      int armor,
      CreatureType[] types = null,
      Keyword[] keywords = null,
      Delegates.Callback playAbility = null,
      Delegates.Callback fightAbility = null,
      Delegates.Callback creatureAbility = null,
      Delegates.Callback destroyedAbility = null) : base(house, CardType.Creature, playAbility)
    {
      Power = power;
      Armor = armor;
      Keywords = keywords ?? new Keyword[0];
      Types = types ?? new CreatureType[0];
      FightAbility = fightAbility;
      CreatureAbility = creatureAbility;
      DestroyedAbility = destroyedAbility;
    }

    public Creature InsantiateCreature()
    {
      return new Creature(this);
    }
  }
}