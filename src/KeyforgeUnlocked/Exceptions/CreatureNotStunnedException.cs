using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Exceptions
{
  public sealed class CreatureNotStunnedException : KeyforgeUnlockedException
  {
    public Creature Creature;
    
    public CreatureNotStunnedException(IState state, Creature creature) : base(state)
    {
      Creature = creature;
    }
  }
}