using System;
using KeyforgeUnlocked.Cards;

namespace KeyforgeUnlocked.Creatures
{
  public struct Creature
  {
    public CreatureCard Card;

    public int PowerCounters;

    public int Damage;

    public bool IsReady;

    public string Id => Card.Id;

    public int BasePower => Card.Power;

    public int BaseArmor => Card.Armor;
    public int Power => BasePower + PowerCounters;

    public int Health => Power - Damage;

    public Creature(
      CreatureCard card,
      int powerCounters = 0,
      int damage = 0,
      bool isReady = false)
    {
      Card = card;
      PowerCounters = powerCounters;
      Damage = damage;
      IsReady = isReady;
    }

    public bool Equals(Creature other)
    {
      return Id == other.Id
             && PowerCounters == other.PowerCounters
             && Damage == other.Damage
             && IsReady == other.IsReady;
    }

    public override bool Equals(object obj)
    {
      return obj is Creature other && Equals(other);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(
        Id,
        PowerCounters,
        Damage,
        IsReady);
    }
  }
}