using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Effects
{
  public sealed class ReadyAndUse : IEffect
  {
    readonly Creature _target;
    readonly bool _allowOutOfHouseUse;

    public ReadyAndUse(Creature target, bool allowOutOfHouseUse)
    {
      _target = target;
      _allowOutOfHouseUse = allowOutOfHouseUse;
    }

    public void Resolve(MutableState state)
    {
      var target = state.FindCreature(_target.Id, out var controllingPlayer);
      if(state.playerTurn != controllingPlayer)
        throw new InvalidTargetException(state, _target.Id);
      if (!target.IsReady)
      {
        target.IsReady = true;
        state.UpdateCreature(target);
        state.ResolvedEffects.Add(new CreatureReadied(target));
      }
      state.ActionGroups.Add(new UseCreatureGroup(state, target, _allowOutOfHouseUse));
    }
  }
}