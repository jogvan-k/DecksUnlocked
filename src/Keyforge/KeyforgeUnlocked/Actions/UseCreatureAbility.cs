using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Actions
{
    public sealed class UseCreatureAbility : UseCreature<UseCreatureAbility>
    {
        public UseCreatureAbility(ImmutableState origin, Creature creature, bool allowOutOfHouseUse = false)
            : base(origin, creature, allowOutOfHouseUse)
        {
        }

        internal override void Validate(IState state)
        {
            base.Validate(state);
            if (Creature.IsStunned())
                throw new CreatureStunnedException(state, Creature);
        }

        protected override void DoSpecificActionNoResolve(IMutableState state)
        {
            state.Effects.Push(new CreatureAbility(Creature));
        }

        public override string ToString()
        {
            return "Use creature ability";
        }
    }
}