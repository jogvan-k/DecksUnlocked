using System;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Actions
{
  public sealed class FightCreature : UseCreature
  {
    public Creature Target { get; }

    public FightCreature(Creature fighter,
      Creature target) : base(fighter)
    {
      Target = target;
    }

    internal override void Validate(IState state)
    {
      base.Validate(state);
      var playerTurn = state.PlayerTurn;

      if (state.ControllingPlayer(Creature) != playerTurn || state.ControllingPlayer(Target) == playerTurn)
        throw new InvalidFightException(state, Creature, Target);
    }

    internal override void DoActionNoResolve(MutableState state)
    {
      state.Effects.Enqueue(new Effects.FightCreature(Creature, Target));
    }

    bool Equals(FightCreature other)
    {
      return Creature.Equals(other.Creature) && Target.Equals(other.Target);
    }

    public override bool Equals(object obj)
    {
      return ReferenceEquals(this, obj) || obj is FightCreature other && Equals(other);
    }

    public override int GetHashCode()
    {
      return 13 * HashCode.Combine(Creature, Target);
    }
  }
}