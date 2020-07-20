using System;
using UnlockedCore.Actions;

namespace KeyforgeUnlocked.Cards
{
  public abstract class Card
  {
    static readonly Random Random = new Random();
    public string Name { get; }

    public House House { get; }

    public CardType CardType { get; }

    readonly int _hash;

    protected Card(string name,
      House house,
      CardType cardType)
    {
      Name = name;
      House = house;
      CardType = cardType;
      _hash = Random.Next();
    }

    protected bool Equals(Card other)
    {
      return _hash == other._hash;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((Card) obj);
    }

    public override string ToString()
    {
      return Name;
    }

    public override int GetHashCode()
    {
      return _hash;
    }
  }
}