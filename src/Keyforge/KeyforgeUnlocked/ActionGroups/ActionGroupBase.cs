using System.Collections.Immutable;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.ActionGroups
{
    public abstract class ActionGroupBase<T> : Equatable<T>, IActionGroup
    {
        public IImmutableList<IAction> Actions(ImmutableState origin) => InitiateActions(origin);

        protected abstract IImmutableList<IAction> InitiateActions(ImmutableState origin);
    }
}