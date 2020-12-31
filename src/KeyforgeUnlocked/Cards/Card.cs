using System;
using System.Globalization;
using System.Linq;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards
{
  public abstract class Card : Equatable<Card>, ICard
  {
    static readonly StringComparer nameComparer = StringComparer.Create(CultureInfo.CurrentCulture, false);

    public string Id { get; }
    public House House { get; }
    public Pip[] CardPips { get; }
    readonly Lazy<string> _name;
    public string Name => _name.Value;
    public Callback CardPlayAbility { get; }
    public ActionPredicate CardPlayAllowed { get; }

    protected Card(
      House house,
      Pip[] pips = null,
      Callback playAbility = null,
      ActionPredicate PlayAllowed = null,
      string id = null)
    {
      Id = id ?? IdGenerator.GetNextInt().ToString();
      House = house;
      _name = new Lazy<string>(GetName);
      CardPips = pips ?? Array.Empty<Pip>();
      CardPlayAbility = playAbility;
      CardPlayAllowed = PlayAllowed ?? Delegates.True;
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
      return ((IIdentifiable) this).Equals(other);
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