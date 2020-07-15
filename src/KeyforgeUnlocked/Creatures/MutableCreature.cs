using KeyforgeUnlocked.Cards;

namespace KeyforgeUnlocked.Creatures
{
  public class MutableCreature : CreatureBase, ICreature
  {
    public string CreatureId
    {
      get => _creatureId;
      set => _creatureId = value;
    }

    public int BasePower
    {
      get => _basePower;
      set => _basePower = value;
    }

    public int Armor
    {
      get => _armor;
      set => _armor = value;
    }

    public CreatureCard Card
    {
      get => _card;
      set => _card = value;
    }

    public int PowerCounters
    {
      get => _powerCounters;
      set => _powerCounters = value;
    }

    public int Damage
    {
      get => _damage;
      set => _damage = value;
    }

    public bool IsReady
    {
      get => _isReady;
      set => _isReady = value;
    }

    public int Power => BasePower + PowerCounters;

    public int Health => Power - Damage;

    public MutableCreature(
      int basePower,
      int armor,
      CreatureCard card) : base(basePower, armor, card)
    {
    }

    public MutableCreature(
      string creatureId,
      int basePower,
      int armor,
      CreatureCard card,
      int powerCounters,
      int damage,
      bool isReady) : this(basePower, armor, card)
    {
      _creatureId = creatureId;
      _powerCounters = powerCounters;
      _damage = damage;
      _isReady = isReady;
    }

    public Creature ToImmutable()
    {
      return new Creature(
        _creatureId,
        _basePower,
        _armor,
        Card,
        _powerCounters,
        _damage,
        _isReady);
    }
  }
}