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
    Pip[] Pips { get; }
    string Name { get; }
    Callback CardPlayAbility { get; }
  }
}