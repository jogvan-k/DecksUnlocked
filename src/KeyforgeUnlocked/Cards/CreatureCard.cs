using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Creatures;
using UnlockedCore.Actions;

namespace KeyforgeUnlocked.Cards
{
  public abstract class CreatureCard : Card
  {
    public int Power { get; }
    public int Armor { get; }

    protected CreatureCard(
      string name,
      House house,
      int power,
      int armor) : base(name, house, CardType.Creature)
    {
      Power = power;
      Armor = armor;
    }

    public Creature InsantiateCreature()
    {
      return new Creature(this);
    }
  }
}