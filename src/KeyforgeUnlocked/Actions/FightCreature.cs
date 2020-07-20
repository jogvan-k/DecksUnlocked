using System;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Actions
{
  public sealed class FightCreature : Action
  {
    public string FighterId { get; }
    public string TargetId { get; }

    Creature _fighter;
    Creature _target;

    public FightCreature(string fighterId,
      string targetId)
    {
      FighterId = fighterId;
      TargetId = targetId;
    }

    internal override void Validate(IState state)
    {
      var playerTurn = state.PlayerTurn;
      _fighter = state.FindCreature(FighterId, out var fightingPlayer);
      _target = state.FindCreature(TargetId, out var targetPlayer);

      if (!_fighter.IsReady)
        throw new CreatureNotReadyException(state, _fighter);

      if (fightingPlayer != playerTurn || targetPlayer == playerTurn)
        throw new InvalidFightException(state, FighterId, TargetId);
    }

    internal override void DoActionNoResolve(MutableState state)
    {
      state.Effects.Enqueue(new Effects.FightCreature(_fighter, _target));
    }

    bool Equals(FightCreature other)
    {
      return FighterId == other.FighterId && TargetId == other.TargetId;
    }

    public override bool Equals(object obj)
    {
      return ReferenceEquals(this, obj) || obj is FightCreature other && Equals(other);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(FighterId, TargetId);
    }
  }
}