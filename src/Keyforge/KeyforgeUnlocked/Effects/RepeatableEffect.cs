using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using UnlockedCore;

namespace KeyforgeUnlocked.Effects
{
  public class RepeatableEffect : EffectBase<RepeatableEffect>
  {
    readonly Callback _effect;
    readonly StatePredicate _statePredicate;
    readonly bool _guaranteeOnce;

    public RepeatableEffect(Callback effect, StatePredicate statePredicate, bool guaranteeOnce)
    {
      _effect = effect;
      _statePredicate = statePredicate;
      _guaranteeOnce = guaranteeOnce;
    }

    protected override void ResolveImpl(IMutableState state)
    {
      if (_guaranteeOnce || _statePredicate(state))
      {
        state.Effects.Push(new RepeatableEffect(_effect, _statePredicate, false));
        _effect(state, null, Player.None);
      }
    }
  }
}