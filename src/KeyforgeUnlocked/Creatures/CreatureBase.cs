using System;
using KeyforgeUnlocked.Cards;

namespace KeyforgeUnlocked.Creatures
{
  public class CreatureBase
  {
    protected string _id;
    protected int _basePower;
    protected int _armor;
    protected CreatureCard _card;
    protected int _powerCounters;
    protected int _damage;
    protected bool _isReady;

    public CreatureBase(int basePower,
      int armor,
      CreatureCard card)
    {
      _id = GenerateId();
      _basePower = basePower;
      _armor = armor;
      _card = card;
      _powerCounters = 0;
      _damage = 0;
      _isReady = true;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (!(obj is CreatureBase)) return false;
      return Equals((CreatureBase) obj);
    }

    bool Equals(CreatureBase other)
    {
      return _basePower == other._basePower
             && _armor == other._armor
             && Equals(_card, other._card)
             && _powerCounters == other._powerCounters
             && _damage == other._damage
             && _isReady == other._isReady;
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(
        _basePower,
        _armor,
        _card,
        _powerCounters,
        _damage,
        _isReady);
    }

    protected static string GenerateId()
    {
      return Guid.NewGuid().ToString("N");
    }
  }
}