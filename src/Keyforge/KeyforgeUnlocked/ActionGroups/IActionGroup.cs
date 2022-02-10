using System.Collections.Immutable;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.ActionGroups
{
    public interface IActionGroup
    {
        public IImmutableList<IAction> Actions(ImmutableState state);
    }
}