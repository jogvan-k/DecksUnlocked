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
    public Callback FightAbility { get; }
    public Callback CreatureAbility { get; }
    public Callback DestroyedAbility { get; }

    protected CreatureCard(
      House house,
      int power,
      int armor,
      CreatureType[] types = null,
      Keyword[] keywords = null,
      Callback playAbility = null,
      Callback fightAbility = null,
      Callback creatureAbility = null,
      Callback destroyedAbility = null) : base(house, CardType.Creature, playAbility)
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