using System;
using System.Globalization;
using System.Reflection;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards
{
  public abstract class Card : IIdentifiable, IComparable<Card>, IComparable
  {
    static readonly StringComparer nameComparer = StringComparer.Create(CultureInfo.CurrentCulture, false);

    // TODO reconsider use of Id
    public string Id { get; }

    public House House { get; }


    readonly Lazy<string> _name;
    public string Name => _name.Value;

    public CardType CardType { get; }

    public Callback PlayAbility { get; }

    protected Card(
      House house,
      CardType cardType,
      Callback playAbility = null)
    {
      Id = IdGenerator.GetNextInt().ToString();//Guid.NewGuid().ToString("N");
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
      return Id.Equals(other.Id);
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

    public int CompareTo(object obj)
    {
      if (obj == null)
        return 1;
      return CompareTo((Card) obj);
    }

    public int CompareTo(Card other)
    {
      return nameComparer.Compare(Id, other.Id);
    }

    public override int GetHashCode()
    {
      return Id.GetHashCode();
    }
  }
}