using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;
using KeyforgeUnlockedTest.Effects;
using UnlockedCore.States;

namespace KeyforgeUnlocked.Creatures
{
  public static class CreatureUtil
  {
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
      }

      throw new CreatureNotPresentException(state, creature.Id);
    }

    public static void DestroyCreature(
      MutableState state,
      Creature creature)
    {
      var owningPlayer = RemoveCreature(state, creature);
      state.Discards[owningPlayer].Add(creature.Card);
      state.ResolvedEffects.Add(new CreatureDied(creature));
    }

    static Player RemoveCreature(
      MutableState state,
      Creature creature)
    {
      foreach (var player in state.Fields.Keys)
      {
        foreach (var c in state.Fields[player])
        {
          if (c.Id == creature.Id)
          {
            state.Fields[player].Remove(c);
            return player;
          }
        }
      }

      throw new CreatureNotPresentException(state, creature.Id);
    }
  }
}