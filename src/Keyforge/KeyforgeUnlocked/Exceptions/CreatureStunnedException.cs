using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Exceptions
{
    public sealed class CreatureStunnedException : KeyforgeUnlockedException
    {
        public Creature Creature;

        public CreatureStunnedException(IState state, Creature creature) : base(state)
        {
            Creature = creature;
        }
    }
}