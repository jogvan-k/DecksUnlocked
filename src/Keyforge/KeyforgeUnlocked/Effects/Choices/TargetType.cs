using System;

namespace KeyforgeUnlocked.Effects.Choices
{
    [Flags]
    public enum TargetType
    {
        Creature = 1,
        Artifact = 2,
        CardInHand = 4,
        CardInDiscard = 8
    }
}