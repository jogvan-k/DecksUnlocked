using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Creatures;
using UnlockedCore.Actions;

namespace KeyforgeUnlocked.Cards
{
  public abstract class CreatureCard : Card
  {
    public int Power { get; }
    public int Armor { get; }
    
    public Keyword[] Keywords { get; }

    protected CreatureCard(
      string name,
      House house,
      int power,
      int armor,
      Keyword[] keywords) : base(name, house, CardType.Creature)
    {
      Power = power;
      Armor = armor;
      Keywords = keywords;
    }

    public Creature InsantiateCreature()
    {
      return new Creature(this);
    }
  }
}