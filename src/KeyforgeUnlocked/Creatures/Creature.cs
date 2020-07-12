using System;
using KeyforgeUnlocked.Cards;

namespace KeyforgeUnlocked.Creatures
{
  public class Creature
  {
    public int BasePower { get; }
    public int Armor { get; }
    public CreatureCard Card { get; }
    public int PowerCounters { get; }
    public int Power => BasePower + PowerCounters;
    public int Damage { get; }
    public int Health => Power - Damage;

    public Creature(int basePower,
      int armor,
      CreatureCard card)
    {
      BasePower = basePower;
      Armor = armor;
      Card = card;
      PowerCounters = 0;
      Damage = 0;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((Creature) obj);
    }

    bool Equals(Creature other)
    {
      return BasePower == other.BasePower
             && Armor == other.Armor
             && Equals(Card, other.Card)
             && PowerCounters == other.PowerCounters
             && Damage == other.Damage;
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(
        BasePower, Armor, Card,
        PowerCounters, Damage);
    }
  }
}