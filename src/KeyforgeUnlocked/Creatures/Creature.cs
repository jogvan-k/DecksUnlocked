using System;
using KeyforgeUnlocked.Cards;

namespace KeyforgeUnlocked.Creatures
{
  public class Creature : CreatureBase, ICreature
  {
    public string Id => _id;

    public int BasePower => _basePower;

    public int Armor => _armor;

    public CreatureCard Card => _card;

    public int PowerCounters => _powerCounters;

    public int Damage => _damage;

    public int Power => BasePower + PowerCounters;

    public bool IsReady => _isReady;

    public int Health => Power - Damage;

    public Creature(int basePower,
      int armor,
      CreatureCard card) : base(basePower, armor, card)
    {
    }

    public Creature(
      string id,
      int basePower,
      int armor,
      CreatureCard card,
      int powerCounters,
      int damage,
      bool isReady) : this(basePower, armor, card)
    {
      _id = id;
      _powerCounters = powerCounters;
      _damage = damage;
      _isReady = isReady;
    }

    public MutableCreature ToMutable()
    {
      return new MutableCreature(
        _id ,
        _basePower,
        _armor,
        Card,
        _powerCounters,
        _damage,
        _isReady);
    }
  }
}