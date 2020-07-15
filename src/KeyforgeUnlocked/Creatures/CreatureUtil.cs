using System.Linq;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Effects
{
  public static class CreatureUtil
  {
    public static void FindAndValidateCreatureReady(
      IState state,
      string creatureId,
      out Creature creature)
    {
      creature = FindCreature(state, creatureId);

      if (!creature.IsReady)
        throw new CreatureNotReadyException(creature, state);
    }

    public static Creature FindCreature(
      IState state,
      string creatureId)
    {
      foreach (var creature in state.Fields.Values.SelectMany(v => v))
      {
        if (creature.Id.Equals(creatureId))
          return creature;
      }

      throw new CreatureNotPresentException(creatureId, state);
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
            creatures[i] = creature;
        }
      }
    }
  }
}