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

    protected override void SpecificResolve(IMutableState state, Creature fighter)
    {
      ResolveBeforeFightEffects(state, fighter);

      if (!state.TryFindCreature(Target, out var opponentPlayer, out _, out var targetCreature)
          || !state.TryFindCreature(fighter, out var fightingPlayer, out _, out fighter))
        return;
      
      if (!targetCreature.CardKeywords.Contains(Keyword.Elusive) || state.HistoricData.CreaturesAttackedThisTurn.Contains(new Identifiable(targetCreature)))
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
        fighter.FightAbility?.Invoke(state, fighter, fightingPlayer);
        if(targetCreature.IsDead)
          fighter.AfterKillAbility?.Invoke(state, fighter, fightingPlayer);
      }
      if(!targetCreature.IsDead && fighter.IsDead)
        targetCreature.AfterKillAbility?.Invoke(state, targetCreature, opponentPlayer);

      state.HistoricData.CreaturesAttackedThisTurn = state.HistoricData.CreaturesAttackedThisTurn.Add(new Identifiable(targetCreature));
      if (targetCreature.IsDead)
        state.HistoricData.EnemiesDestroyedInAFightThisTurn += 1;
    }

    void ResolveBeforeFightEffects(IMutableState state, Creature fighter)
    {
      fighter.Card.CardBeforeFightAbility?.Invoke(state, fighter, state.PlayerTurn);
    }

    protected override bool Equals(FightCreature other)
    {
      return base.Equals(other) && Target.Equals(other.Target);
    }
  }
}