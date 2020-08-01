using System;
using System.Linq;
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
      fighter.IsReady = false;
      if (!target.Keywords.Contains(Keyword.Elusive) || state.HasEffectOccured(HasBeenAttacked(target.Id)))
      {
        int damageBeforeFight;
        if (!fighter.Keywords.Contains(Keyword.Skirmish))
        {
          damageBeforeFight = fighter.Damage;
          fighter = fighter.Damage(target.Power);
          if (target.Keywords.Contains(Keyword.Poison) && fighter.Damage > damageBeforeFight)
            fighter.Damage += 1000;
        }

        damageBeforeFight = target.Damage;
        target = target.Damage(fighter.Power);
        if (fighter.Keywords.Contains(Keyword.Poison) && target.Damage > damageBeforeFight)
          target.Damage += 1000;
      }

      state.ResolvedEffects.Add(new CreatureFought(fighter, target));
      state.UpdateCreature(fighter);
      state.UpdateCreature(target);

      if (!fighter.IsDead)
      {
        fighter.FightAbility?.Invoke(state, fighter.Id);
        if(target.IsDead)
          fighter.AfterKillAbility?.Invoke(state, fighter.Id);
      }
      if(!target.IsDead && fighter.IsDead)
        target.AfterKillAbility?.Invoke(state, target.Id);
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