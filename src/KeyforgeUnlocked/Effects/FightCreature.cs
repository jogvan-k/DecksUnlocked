using System;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Effects
{
  public sealed class FightCreature : IEffect
  {
    public Creature Fighter { get; }

    public Creature Target { get; }

    public FightCreature(Creature fighter,
      Creature target)
    {
      Fighter = fighter;
      Target = target;
    }

    public void Resolve(MutableState state)
    {
      var fighter = Fighter;
      var target = Target;
      fighter.Damage += target.Power;
      target.Damage += fighter.Power;

      fighter.IsReady = false;
      state.ResolvedEffects.Add(new CreatureFought(fighter, target));
      UpdateState(state, fighter, target);
    }

    void UpdateState(MutableState state,
      Creature fighter,
      Creature target)
    {
      UpdateOrDestroy(state, fighter);
      UpdateOrDestroy(state, target);
    }

    void UpdateOrDestroy(MutableState state,
      Creature creature)
    {
      if(creature.Health > 0)
        CreatureUtil.SetCreature(state, creature);
      else
      {
        CreatureUtil.DestroyCreature(state, creature);
      }
    }

    bool Equals(FightCreature other)
    {
      return Equals(Fighter, other.Fighter) && Equals(Target, other.Target);
    }

    public override bool Equals(object obj)
    {
      return ReferenceEquals(this, obj) || obj is FightCreature other && Equals(other);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(Fighter, Target);
    }
  }
}