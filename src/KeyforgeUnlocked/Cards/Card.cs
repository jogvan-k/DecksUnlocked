using System;
using System.Reflection;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards
{
  public abstract class Card : IIdentifiable
  {
    public string Id { get; }

    public House House { get; }


    Lazy<string> _name;
    public string Name => _name.Value;

    public CardType CardType { get; }

    public Delegates.Callback PlayAbility { get; }

    protected Card(
      House house,
      CardType cardType,
      Delegates.Callback playAbility = null)
    {
      Id = Guid.NewGuid().ToString("N");
      House = house;
      _name = new Lazy<string>(GetName);
      CardType = cardType;
      PlayAbility = playAbility;
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