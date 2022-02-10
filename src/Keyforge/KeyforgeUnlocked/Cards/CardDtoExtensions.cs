using System;
using System.Collections.Generic;
using System.Linq;

namespace KeyforgeUnlocked.Cards
{
    public static class CardDtoExtensions
    {
        static Lazy<IDictionary<string, Type>>
            CardClasses = new Lazy<IDictionary<string, Type>>(() => AllCardClasses());

        public static CardDto ToDto(this ICard card) =>
            new()
            {
                Name = card.Name,
                House = card.House,
                Id = card.Id
            };

        public static ICard ToCard(this CardDto dto)
        {
            var @class = CardClasses.Value[dto.Name];
            var card = (ICard)@class.GetConstructor(new Type[] { typeof(House) })
                ?.Invoke(new object[] { dto.House });
            @class.GetProperty("Id").SetValue(card, dto.Id);
            return card;
        }


        static IDictionary<string, Type> AllCardClasses()
        {
            var allCardTypes = from type in AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes())
                where typeof(Card).IsAssignableFrom(type)
                select type;

            return allCardTypes.ToDictionary(t => Card.GetName(t), t => t);
        }
    }
}