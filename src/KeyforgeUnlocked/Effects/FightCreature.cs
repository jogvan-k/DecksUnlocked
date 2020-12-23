using System;
using System.Linq;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.States.Extensions;

namespace KeyforgeUnlocked.Effects
{
  public sealed class FightCreature : UseCreature<FightCreature>
  {
    public string TargetId { get; }

    public FightCreature(Creature fighter, string targetId) : base(fighter)
    {
      TargetId = targetId;
    }

    protected override void SpecificResolve(MutableState state, Creature fighter)
    {
      ResolveBeforeFightEffects(state, fighter);

      if (!state.TryFindCreature(TargetId, out _, out _, out var target)
          || !state.TryFindCreature(fighter.Id, out _, out _, out fighter))
        return;
      
      if (!target.CardKeywords.Contains(Keyword.Elusive) || state.HasEffectOccured(HasBeenAttacked(target.Id)))
      {
        int damageBeforeFight;
        if (!fighter.CardKeywords.Contains(Keyword.Skirmish))
        {
          damageBeforeFight = fighter.Damage;
          fighter = fighter.Damage(target.Power);
          if (target.CardKeywords.Contains(Keyword.Poison) && fighter.Damage > damageBeforeFight)
            fighter.Damage += 1000;
        }

        damageBeforeFight = target.Damage;
        target = target.Damage(fighter.Power);
        if (fighter.CardKeywords.Contains(Keyword.Poison) && target.Damage > damageBeforeFight)
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

    void ResolveBeforeFightEffects(MutableState state, Creature fighter)
    {
      fighter.Card.CardBeforeFightAbility?.Invoke(state, fighter.Id);
    }

    Predicate<IResolvedEffect> HasBeenAttacked(string creatureId)
    {
      return re => re is CreatureFought cf && cf.Target.Id.Equals(creatureId);
    }

    protected override bool Equals(FightCreature other)
    {
      return base.Equals(other) && TargetId.Equals(other.TargetId);
    }
  }
}