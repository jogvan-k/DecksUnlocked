using System;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards
{
  /// <summary>
  /// Interface for classes representing a card.
  /// </summary>
  public interface ICard : IIdentifiable, IComparable<Card>, IComparable
  {
    House House { get; }
    Pip[] CardPips { get; }
    Callback CardPlayAbility { get; }
  }
}