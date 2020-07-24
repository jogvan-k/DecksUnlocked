using System;
using System.Reflection;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards
{
  public abstract class Card : IIdentifiable
  {
    public string Id { get; }

    public House House { get; }

    public CardType CardType { get; }

    Lazy<string> _name;

    public string Name => _name.Value;

    protected Card(
      House house,
      CardType cardType)
    {
      Id = Guid.NewGuid().ToString("N");
      House = house;
      CardType = cardType;
      _name = new Lazy<string>(GetName);
    }

    string GetName()
    {
      var fieldInfo = GetType().GetField("SpecialName", BindingFlags.Public | BindingFlags.Static);
      if (fieldInfo == null)
        return GetType().Name;
      return (string) fieldInfo.GetValue(null);
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