using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Types;
using UnlockedCore.Actions;
using Action = System.Action;

namespace KeyforgeUnlocked.Cards
{
  public abstract class CreatureCard : Card
  {
    public int Power { get; }
    public int Armor { get; }
    public Keyword[] Keywords { get; }
    public Delegates.Callback FightAbility { get; }

    protected CreatureCard(
      string name,
      House house,
      int power,
      int armor,
      Keyword[] keywords,
      Delegates.Callback fightAbility) : base(name, house, CardType.Creature)
    {
      Power = power;
      Armor = armor;
      Keywords = keywords;
      FightAbility = fightAbility ?? Delegates.NoChange;
    }

    public Creature InsantiateCreature()
    {
      return new Creature(this);
    }
  }
}