using System;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Actions
{
  public sealed class FightCreature : UseCreature<FightCreature>
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

    protected override void DoSpecificActionNoResolve(IMutableState state)
    {
      state.Effects.Enqueue(new Effects.FightCreature(Creature, Target));
    }

    public override string Identity()
    {
      _origin.FindCreature(Target, out _, out var index);

      return base.Identity() + ';' + index;
    }

    protected override bool Equals(FightCreature other)
    {
      return base.Equals(other) && Target.Equals(other.Target);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(base.GetHashCode(), Target);
    }
  }
}