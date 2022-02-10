using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Exceptions
{
    public sealed class InvalidFightException : KeyforgeUnlockedException
    {
        public Creature FightingCreature { get; }
        public Creature TargetCreature { get; }

        public InvalidFightException(IState state,
            Creature fightingCreature,
            Creature targetCreature) : base(state)
        {
            FightingCreature = fightingCreature;
            TargetCreature = targetCreature;
        }
    }
}