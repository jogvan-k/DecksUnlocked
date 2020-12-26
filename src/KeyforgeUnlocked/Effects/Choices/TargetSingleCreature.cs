using System.Collections.Generic;
using System.Linq;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Effects.Choices
{
  public sealed class TargetSingleCreature : TargetSingle<Creature>
  {

    public TargetSingleCreature(EffectOnTarget effect, ValidOn validOn) : base(effect, validOn)
    {
    }

    protected override IEnumerable<IIdentifiable> UnfilteredTargets(IState state)
    {
      return state.Fields[state.PlayerTurn.Other()]
        .Concat(state.Fields[state.PlayerTurn])
        .Select(c => (IIdentifiable)c);
    }
  }
}