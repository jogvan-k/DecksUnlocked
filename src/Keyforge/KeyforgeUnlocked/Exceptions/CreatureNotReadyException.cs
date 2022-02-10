using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Exceptions
{
    public class CreatureNotReadyException : KeyforgeUnlockedException
    {
        public Creature Creature;

        public CreatureNotReadyException(IState state, Creature creature) : base(state)
        {
            Creature = creature;
        }
    }
}