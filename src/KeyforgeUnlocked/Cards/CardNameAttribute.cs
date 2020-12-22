using System;

namespace KeyforgeUnlocked.Cards
{
  [AttributeUsage(AttributeTargets.Class)]
  public class CardNameAttribute : Attribute
  {
    public readonly string cardName;

    public CardNameAttribute(string cardName)
    {
      this.cardName = cardName;
    }
  }
}