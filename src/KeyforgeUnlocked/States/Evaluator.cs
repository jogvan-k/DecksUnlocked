using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using KeyforgeUnlocked.Creatures;
using UnlockedCore;

namespace KeyforgeUnlocked.States
{
  public class Evaluator : IEvaluator
  {
    const int GameWon = 1000;
    const int Key = 200;
    const int Aember = 20;
    const int Creature = 10;
    const int Artifact = 1;
    const int CreatureEnraged = -5;
    const int CreatureStunned = -8;
    const int CreatureWarded = 8;
    const int capturedAember = -4;

    public int Evaluate(ICoreState state)
    {
      return Evaluate((IState) state);
    }

    int Evaluate(IState state)
    {
      if (state.Keys[Player.Player1] >= 3)
        return GameWon;
      if (state.Keys[Player.Player2] >= 3)
        return -GameWon;
      var value = 0;

      value += Key * (state.Keys[Player.Player1] - state.Keys[Player.Player2]);
      value += Aember * (state.Aember[Player.Player1] - state.Aember[Player.Player2]);
      value += Creature * (MaxCountCreatureOfSameHouse(state.Fields[Player.Player1]) -
                           MaxCountCreatureOfSameHouse(state.Fields[Player.Player2]));
      value += Artifact * (state.Artifacts[Player.Player1].Count - state.Artifacts[Player.Player2].Count);
      value += MapStatusEffects(state.Fields[Player.Player1]) - MapStatusEffects(state.Fields[Player.Player2]);

      return value;
    }

    static int MaxCountCreatureOfSameHouse(IEnumerable<Creature> field)
    {
      return field.GroupBy(c => c.Card.House).Select(g => g.Count()).Append(0).Max();
    }

    int MapStatusEffects(IImmutableList<Creature> field)
    {
      return field.Sum(c => SumStatusEffects(c));
    }

    int SumStatusEffects(Creature creature)
    {
      var value = 0;
      if ((creature.State & CreatureState.Enraged) != 0)
        value += CreatureEnraged;
      if ((creature.State & CreatureState.Stunned) != 0)
        value += CreatureStunned;
      if ((creature.State & CreatureState.Warded) != 0)
        value += CreatureWarded;
      value += capturedAember * creature.Aember;
      return value;
    }
  }
}