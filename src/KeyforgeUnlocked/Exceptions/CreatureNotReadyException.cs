using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Exceptions
{
  public class CreatureNotReadyException : KeyforgeUnlockedException
  {
    public ICreature Creature;
    public CreatureNotReadyException(ICreature creature, IState state) : base(state)
    {
      Creature = creature;
    }
  }
}