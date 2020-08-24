using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Exceptions
{
  public class CreatureNotPresentException : KeyforgeUnlockedException
  {
    public string CreatureId;
    public CreatureNotPresentException(IState state, string creature) : base(state)
    {
      CreatureId = creature;
    }
  }
}