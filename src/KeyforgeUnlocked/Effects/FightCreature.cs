using System;
using System.Linq;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Effects
{
  public sealed class FightCreature : UseCreature<FightCreature>
  {
    public IIdentifiable Target { get; }

    public FightCreature(Creature fighter, IIdentifiable target) : base(fighter)
    {
      Target = target;
    }

    protected override void SpecificResolve(MutableState state, Creature fighter)
    {
      ResolveBeforeFightEffects(state, fighter);

      if (!state.TryFindCreature(Target, out _, out _, out var targetCreature)
          || !state.TryFindCreature(fighter, out _, out _, out fighter))
        return;
      
      if (!targetCreature.CardKeywords.Contains(Keyword.Elusive) || state.HasEffectOccured(HasBeenAttacked(Target)))
      {
        int damageBeforeFight;
        if (!fighter.CardKeywords.Contains(Keyword.Skirmish))
        {
          damageBeforeFight = fighter.Damage;
          fighter = fighter.Damage(targetCreature.Power);
          if (targetCreature.CardKeywords.Contains(Keyword.Poison) && fighter.Damage > damageBeforeFight)
            fighter.Damage += 1000;
        }

        damageBeforeFight = targetCreature.Damage;
        targetCreature = targetCreature.Damage(fighter.Power);
        if (fighter.CardKeywords.Contains(Keyword.Poison) && targetCreature.Damage > damageBeforeFight)
          targetCreature.Damage += 1000;
      }

      state.ResolvedEffects.Add(new CreatureFought(fighter, targetCreature));
      state.UpdateCreature(fighter);
      state.UpdateCreature(targetCreature);

      if (!fighter.IsDead)
      {
        fighter.FightAbility?.Invoke(state, fighter);
        if(targetCreature.IsDead)
          fighter.AfterKillAbility?.Invoke(state, fighter);
      }
      if(!targetCreature.IsDead && fighter.IsDead)
        targetCreature.AfterKillAbility?.Invoke(state, targetCreature);

      if (targetCreature.IsDead)
        state.HistoricData.EnemiesDestroyedInAFightThisTurn += 1;
    }

    void ResolveBeforeFightEffects(MutableState state, Creature fighter)
    {
      fighter.Card.CardBeforeFightAbility?.Invoke(state, fighter);
    }

    Predicate<IResolvedEffect> HasBeenAttacked(IIdentifiable creatureId)
    {
      return re => re is CreatureFought cf && creatureId.Equals(cf.Target);
    }

    protected override bool Equals(FightCreature other)
    {
      return base.Equals(other) && Target.Equals(other.Target);
    }
  }
}