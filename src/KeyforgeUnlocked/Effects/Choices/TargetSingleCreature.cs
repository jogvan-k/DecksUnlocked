using System.Collections.Generic;
using System.Linq;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using UnlockedCore;

namespace KeyforgeUnlocked.Effects.Choices
{
  public sealed class TargetSingleCreature : TargetSingle
  {

    public TargetSingleCreature(Callback effect, Target targets = Target.All, ValidOn? validOn = null) : base(effect, TargetType.Creature, targets, validOn)
    {
    }
  }
}