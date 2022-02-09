using System;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards
{
  /// <summary>
  /// Interface for classes representing a card.
  /// </summary>
  public interface ICard : IIdentifiable, IComparable<Card>
  {
    House House { get; }
    Pip[] CardPips { get; }
    Callback? CardPlayAbility { get; }
    public ActionPredicate CardPlayAllowed { get; }
  }
}