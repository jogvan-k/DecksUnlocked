using System;
using System.Linq;
using KeyforgeUnlocked.Cards;
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
      if (!target.Keywords.Contains(Keyword.Elusive) || state.HasEffectOccured(HasBeenAttacked(target.Id)))
      {
        if (!fighter.Keywords.Contains(Keyword.Skirmish))
          fighter.Damage += target.Power;
        target.Damage += fighter.Power;
      }

      fighter.IsReady = false;
      state.ResolvedEffects.Add(new CreatureFought(fighter, target));
      state.UpdateCreature(fighter);
      state.UpdateCreature(target);

      if (fighter.Health > 0)
        fighter.FightAbility?.Invoke(state);
    }

    Predicate<IResolvedEffect> HasBeenAttacked(string creatureId)
    {
      return re => re is CreatureFought cf && cf.Target.Id.Equals(creatureId);
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