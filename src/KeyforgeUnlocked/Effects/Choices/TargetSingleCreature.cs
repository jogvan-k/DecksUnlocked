using System.Collections.Generic;
using System.Linq;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using UnlockedCore;

namespace KeyforgeUnlocked.Effects.Choices
{
  public sealed class TargetSingleCreature : TargetSingle
  {

    public TargetSingleCreature(Callback effect, Targets targets = Targets.All, ValidOn validOn = null) : base(effect, targets, validOn)
    {
    }

    protected override IReadOnlyDictionary<Player, IEnumerable<IIdentifiable>> UnfilteredTargets(IState state)
    {
      return state.Fields.ToReadOnly(kv => kv.Value.Cast<IIdentifiable>());
    }
  }
}