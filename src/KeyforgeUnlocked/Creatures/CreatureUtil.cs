using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;
using KeyforgeUnlockedTest.Effects;
using UnlockedCore.States;

namespace KeyforgeUnlocked.Creatures
{
  public static class CreatureUtil
  {
    public static void FindAndValidateCreatureReady(
      IState state,
      string creatureId,
      out Creature creature)
    {
      creature = state.FindCreature(creatureId, out _);

      if (!creature.IsReady)
        throw new CreatureNotReadyException(state, creature);
    }

    public static void SetCreature(
      MutableState state,
      Creature creature)
    {
      foreach (var keyValue in state.Fields)
      {
        var creatures = keyValue.Value;
        for (int i = 0; i < creatures.Count; i++)
        {
          if (creatures[i].Id == creature.Id)
          {
            creatures[i] = creature;
            return;
          }
        }

        throw new CreatureNotPresentException(state, creature.Id);
      }
    }

    public static void DestroyCreature(
      MutableState state,
      Creature creature)
    {
      var owningPlayer = RemoveCreature(state, creature.Id);
      state.Discards[owningPlayer].Add(creature.Card);
      state.ResolvedEffects.Add(new CreatureDied(creature));
    }

    static Player RemoveCreature(
      MutableState state,
      string creatureId)
    {
      foreach (var player in state.Fields.Keys)
      {
        foreach (var creature in state.Fields[player])
        {
          if (creature.Id == creatureId)
          {
            state.Fields[player].Remove(creature);
            return player;
          }
        }
      }

      throw new CreatureNotPresentException(state, creatureId);
    }
  }
}