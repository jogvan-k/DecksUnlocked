using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Effects
{
  public abstract class UseCreature : IEffect
  {
    public readonly string CreatureId;
    protected Creature Creature;

    public UseCreature(string creatureId)
    {
      CreatureId = creatureId;
    }

    public void Resolve(MutableState state)
    {
      CreatureUtil.FindAndValidateCreatureReady(state, CreatureId, out var creature);
      creature.IsReady = false;
      Creature = creature;
      SpecificResolve(state);
      CreatureUtil.SetCreature(state, creature);
    }

    protected abstract void SpecificResolve(MutableState state);
  }
}