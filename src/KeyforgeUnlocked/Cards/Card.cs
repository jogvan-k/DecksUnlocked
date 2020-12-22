using System;
using System.Globalization;
using System.Linq;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards
{
  public abstract class Card : Equatable<Card>, IIdentifiable, IComparable<Card>, IComparable
  {
    static readonly StringComparer nameComparer = StringComparer.Create(CultureInfo.CurrentCulture, false);

    // TODO reconsider use of Id
    public string Id { get; }

    public House House { get; }


    readonly Lazy<string> _name;
    public string Name => _name.Value;

    public CardType CardType { get; }

    public Callback CardPlayAbility { get; }

    protected Card(
      House house,
      CardType cardType,
      Callback playAbility = null)
    {
      Id = IdGenerator.GetNextInt().ToString();//Guid.NewGuid().ToString("N");
      House = house;
      _name = new Lazy<string>(GetName);
      CardType = cardType;
      CardPlayAbility = playAbility;
    }

    string GetName()
    {
      return GetName(GetType());
    }

    public static string GetName(Type card)
    {
      var nameAttribute = Attribute.GetCustomAttribute(card, typeof(CardNameAttribute));
      if (nameAttribute != null)
        return ((CardNameAttribute) nameAttribute).cardName;
      return ToProperCase(card.Name);
    }

    static string ToProperCase(string str)
    {
      if (str == null) return null;
      if (str.Length < 2) return str.ToUpper();

      var properStr = str.Substring(0, 1).ToUpper();

      foreach (var c in str.Skip(1))
      {
        if (char.IsUpper(c))
          properStr += ' ';
        
        properStr += c;
      }

      return properStr;
    }

    protected override bool Equals(Card other)
    {
      return Id.Equals(other.Id);
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