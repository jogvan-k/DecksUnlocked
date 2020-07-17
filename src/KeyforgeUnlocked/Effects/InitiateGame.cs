using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Effects
{
  public class InitiateGame : IEffect
  {
    public void Resolve(MutableState state)
    {
      state.Effects.Enqueue(new DrawInitialHands());
      state.Effects.Enqueue(new DeclareHouse());
      state.Effects.Enqueue(new FirstTurn());
      state.Effects.Enqueue(new EndTurn());
    }

    protected bool Equals(InitiateGame other)
    {
      return true;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((InitiateGame) obj);
    }

    public override int GetHashCode()
    {
      return typeof(InitiateGame).GetHashCode();
    }
  }
}