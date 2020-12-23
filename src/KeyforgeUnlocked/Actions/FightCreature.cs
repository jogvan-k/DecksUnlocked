using System;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Actions
{
  public sealed class FightCreature : UseCreature
  {
    public Creature Target { get; }

    public FightCreature(
      ImmutableState origin,
      Creature fighter,
      Creature target, bool allowOutOfHouseUse = false) : base(origin, fighter, allowOutOfHouseUse)
    {
      Target = target;
    }

    internal override void Validate(IState state)
    {
      base.Validate(state);
      if (Creature.IsStunned())
        throw new CreatureStunnedException(state, Creature);

      var playerTurn = state.PlayerTurn;

      if (state.ControllingPlayer(Creature) != playerTurn || state.ControllingPlayer(Target) == playerTurn)
        throw new InvalidFightException(state, Creature, Target);
    }

    internal override void DoActionNoResolve(MutableState state)
    {
      state.Effects.Enqueue(new Effects.FightCreature(Creature, Target.Id));
    }

    public override string Identity()
    {
      _origin.FindCreature(Target.Id, out _, out var index);

      return base.Identity() + ';' + index;
    }

    protected override bool Equals(BasicAction other)
    {
      return base.Equals(other) && Target.Equals(((FightCreature)other).Target);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(base.GetHashCode(), Target);
    }
  }
}