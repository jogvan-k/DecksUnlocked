using System.Collections.Generic;
using System.Linq;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Effects.Choices
{
  public class TargetSingleDiscardedCard : TargetSingle<ICard>
  {
    public TargetSingleDiscardedCard(EffectOnTarget effect, ValidOn validOn) : base(effect, validOn)
    {
    }

    protected override IEnumerable<IIdentifiable> UnfilteredTargets(IState state)
    {
      return state.Discards[state.PlayerTurn.Other()]
        .Concat(state.Discards[state.PlayerTurn]);
    }
  }
}