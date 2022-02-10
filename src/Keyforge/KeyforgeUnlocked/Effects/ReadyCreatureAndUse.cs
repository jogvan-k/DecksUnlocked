using System;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Effects
{
    public sealed class ReadyCreatureAndUse : EffectWithIdentifiable<ReadyCreatureAndUse>
    {
        readonly bool _allowOutOfHouseUse;
        readonly UseCreature _allowedCreatureUsage;

        public ReadyCreatureAndUse(IIdentifiable creature, bool allowOutOfHouseUse,
            UseCreature allowedCreatureUsage = UseCreature.All) : base(creature)
        {
            _allowOutOfHouseUse = allowOutOfHouseUse;
            _allowedCreatureUsage = allowedCreatureUsage;
        }

        protected override void ResolveImpl(IMutableState state)
        {
            var creature = state.FindCreature(Id, out var controllingPlayer, out _);
            if (state.PlayerTurn != controllingPlayer)
                throw new InvalidTargetException(state, Id);
            if (!creature.IsReady)
            {
                creature.IsReady = true;
                state.UpdateCreature(creature);
                state.ResolvedEffects.Add(new CreatureReadied(creature));
            }

            state.ActionGroups.Add(new UseCreatureGroup(state, creature, _allowOutOfHouseUse, _allowedCreatureUsage));
        }

        protected override bool Equals(ReadyCreatureAndUse other)
        {
            return base.Equals(other) && _allowOutOfHouseUse.Equals(other._allowOutOfHouseUse);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), _allowOutOfHouseUse);
        }
    }
}