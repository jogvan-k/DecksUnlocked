using System.Collections.Generic;
using System.Linq;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using UnlockedCore;

namespace KeyforgeUnlocked.Effects.Choices
{
  public class TargetSingleDiscardedCard : TargetSingle
  {
    public TargetSingleDiscardedCard(EffectOnTarget effect, Targets targets = Targets.All, ValidOn validOn = null) : base(effect, targets, validOn)
    {
    }


    protected override IReadOnlyDictionary<Player, IEnumerable<IIdentifiable>> UnfilteredTargets(IState state)
    {
      return state.Discards.ToReadOnly(kv => kv.Value.Cast<IIdentifiable>());
    }
  }
}