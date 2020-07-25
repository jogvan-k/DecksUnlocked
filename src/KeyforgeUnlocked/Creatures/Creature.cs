using System;
using KeyforgeUnlocked.Cards;
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
    public CreatureState State;

    public string Id => Card.Id;
    public int BasePower => Card.Power;
    public int BaseArmor => Card.Armor;
    public int Power => BasePower + PowerCounters;
    public int Armor => BaseArmor - BrokenArmor;
    public int Health => Power - Damage;
    public Callback FightAbility => Card.FightAbility;
    public Callback DestroyedAbility => Card.DestroyedAbility;
    public Keyword[] Keywords => Card.Keywords;
    public CreatureType[] Types => Card.Types;

    public Creature(
      CreatureCard card,
      int powerCounters = 0,
      int damage = 0,
      int brokenArmor = 0,
      bool isReady = false,
      CreatureState state = CreatureState.None)
    {
      Card = card;
      PowerCounters = powerCounters;
      Damage = damage;
      BrokenArmor = brokenArmor;
      IsReady = isReady;
      State = state;
    }

    public bool Equals(Creature other)
    {
      return Id == other.Id
             && PowerCounters == other.PowerCounters
             && Damage == other.Damage
             && State == other.State
             && IsReady == other.IsReady
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
        BrokenArmor);
    }

    public override string ToString()
    {
      return
        $"{{{Card.GetType().Name}, Power: {Power}, Armor: {Armor}, Health: {Health}, State: {State} IsReady: {IsReady} }}";
    }
  }
}