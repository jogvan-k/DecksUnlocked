using System;
using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Creatures
{
  public struct Creature : IIdentifiable
  {
    public readonly CreatureCard Card;
    public int PowerCounters;
    public int Damage;
    public int BrokenArmor;
    public bool IsReady;
    public int Aember;
    public CreatureState State;

    public string Id => Card.Id;
    public int BasePower => Card.Power;
    public int BaseArmor => Card.Armor;
    public int Power => BasePower + PowerCounters;
    public int Armor => BaseArmor - BrokenArmor;
    public int Health => Power - Damage;
    public bool IsDead => Health <= 0;
    public Callback FightAbility => Card.FightAbility;
    public Callback AfterKillAbility => Card.AfterKillAbility;
    public Callback DestroyedAbility => Card.DestroyedAbility;
    public Keyword[] Keywords => Card.Keywords;
    public CreatureType[] Types => Card.Types;

    public Creature(
      CreatureCard card,
      int powerCounters = 0,
      int damage = 0,
      int brokenArmor = 0,
      bool isReady = false,
      int aember = 0,
      CreatureState state = CreatureState.None)
    {
      Card = card;
      PowerCounters = powerCounters;
      Damage = damage;
      BrokenArmor = brokenArmor;
      IsReady = isReady;
      Aember = aember;
      State = state;
    }

    public bool Equals(Creature other)
    {
      return Id == other.Id
             && PowerCounters == other.PowerCounters
             && Damage == other.Damage
             && State == other.State
             && IsReady == other.IsReady
             && Aember == other.Aember
             && BrokenArmor == other.BrokenArmor;
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
        State,
        IsReady,
        Aember,
        BrokenArmor);
    }

    public override string ToString()
    {
      return
        $"{{{Card.GetType().Name}, Power: {Power}, Armor: {Armor}, Health: {Health}, State: {State}, Aember: {Aember}, IsReady: {IsReady} }}";
    }
  }
}