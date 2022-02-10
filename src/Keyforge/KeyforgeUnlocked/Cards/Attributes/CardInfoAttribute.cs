using System;

namespace KeyforgeUnlocked.Cards.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CardInfoAttribute : Attribute
    {
        public readonly string CardName;
        public readonly Rarity Rarity;
        public readonly string? Description;
        public readonly string? FlavorText;

        public CardInfoAttribute(string cardName, Rarity rarity, string? description = null, string? flavorText = null)
        {
            CardName = cardName;
            Rarity = rarity;
            Description = description;
            FlavorText = flavorText;
        }
    }
}