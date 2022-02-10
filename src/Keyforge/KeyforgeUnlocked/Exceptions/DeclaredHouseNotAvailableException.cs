using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Exceptions
{
    public class DeclaredHouseNotAvailableException : KeyforgeUnlockedException
    {
        public House House { get; }

        public DeclaredHouseNotAvailableException(IState state,
            House house) : base(state)
        {
            House = house;
        }
    }
}