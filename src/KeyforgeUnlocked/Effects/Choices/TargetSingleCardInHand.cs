using System.Collections.Generic;
using System.Linq;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using UnlockedCore;

namespace KeyforgeUnlocked.Effects.Choices
{
  public class TargetSingleCardInHand : TargetSingle
  {
    public TargetSingleCardInHand(Callback effect, Targets targets = Targets.All, ValidOn validOn = null) : base(effect, targets, validOn)
    {
    }

    protected override IReadOnlyDictionary<Player, IEnumerable<IIdentifiable>> UnfilteredTargets(IState state)
    {
      return state.Hands.ToReadOnly(kv => kv.Value.Cast<IIdentifiable>());
    }
  }
}