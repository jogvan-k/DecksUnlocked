using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Exceptions
{
    public sealed class NotFromActiveHouseException : KeyforgeUnlockedException
    {
        public ICard Card;
        public House House;

        public NotFromActiveHouseException(IState state,
            ICard card,
            House house) : base(state)
        {
            Card = card;
            House = house;
        }
    }
}