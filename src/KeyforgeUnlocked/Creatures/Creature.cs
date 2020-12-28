using System;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Creatures
{
  public struct Creature : IIdentifiable
  {
    public readonly ICreatureCard Card;
    public int PowerCounters;
    public int Damage;
    public int BrokenArmor;
    public bool IsReady;
    public int Aember;
    public CreatureState State;

    public string Id => Card.Id;
    public string Name => Card.Name;
    public int BasePower => Card.CardPower;
    public int BaseCardArmor => Card.CardArmor;
    public int Power => BasePower + PowerCounters;
    public int Armor => BaseCardArmor - BrokenArmor;
    public int Health => Power - Damage;
    public bool IsDead => Health <= 0;
    public Callback FightAbility => Card.CardFightAbility;
    public Callback AfterKillAbility => Card.CardAfterKillAbility;
    public Callback DestroyedAbility => Card.CardDestroyedAbility;
    public Keyword[] CardKeywords => Card.CardKeywords;

    public Creature(
      ICreatureCard card,
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