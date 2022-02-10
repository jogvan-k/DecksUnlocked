using System;

namespace KeyforgeUnlocked.Creatures
{
    [Flags]
    public enum CreatureState
    {
        None = 0,
        Warded = 0x0001,
        Enraged = 0x0002,
        Stunned = 0x0004
    }
}