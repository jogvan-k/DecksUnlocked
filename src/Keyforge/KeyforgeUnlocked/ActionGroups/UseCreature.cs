using System;

namespace KeyforgeUnlocked.ActionGroups
{
    [Flags]
    public enum UseCreature
    {
        None = 0x0,
        Fight = 0x1,
        Reap = 0x2,
        ActiveAbility = 0x4,
        All = 0x7
    }
}