using System;

namespace KeyforgeUnlocked.Cards
{
  [AttributeUsage(AttributeTargets.Class)]
  public class CardNameAttribute : Attribute
  {
    public readonly string? CardName;

    public CardNameAttribute(string? cardName = null)
    {
      this.CardName = cardName;
    }
  }
}