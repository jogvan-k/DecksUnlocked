using System.Collections.Generic;
using System.Linq;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using UnlockedCore;

namespace KeyforgeUnlocked.Effects.Choices
{
    public class TargetSingleCardInHand : TargetSingle
    {
        public TargetSingleCardInHand(Callback effect, Target targets = Target.All, ValidOn? validOn = null) : base(
            effect,
            TargetType.CardInHand, targets, validOn)
        {
        }
    }
}