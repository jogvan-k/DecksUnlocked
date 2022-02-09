using System.Collections.Generic;
using System.Linq;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using UnlockedCore;

namespace KeyforgeUnlocked.Effects.Choices
{
  public class TargetSingleDiscardedCard : TargetSingle
  {
    public TargetSingleDiscardedCard(Callback effect, Target targets = Target.All, ValidOn? validOn = null) : base(effect, TargetType.CardInDiscard, targets, validOn)
    {
    }
  }
}