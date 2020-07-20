using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Exceptions
{
  public class CreatureNotReadyException : KeyforgeUnlockedException
  {
    public ICreature Creature;
    public CreatureNotReadyException(IState state, ICreature creature) : base(state)
    {
      Creature = creature;
    }
  }
}