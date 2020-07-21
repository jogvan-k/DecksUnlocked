using System;

namespace KeyforgeUnlocked.Cards
{
  public abstract class Card
  {
    public string Id { get; }
    public string Name { get; }

    public House House { get; }

    public CardType CardType { get; }

    protected Card(string name,
      House house,
      CardType cardType)
    {
      Id = Guid.NewGuid().ToString("N");
      Name = name;
      House = house;
      CardType = cardType;
    }

    protected bool Equals(Card other)
    {
      return Id == other.Id;
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
      return Id.GetHashCode();
    }
  }
}