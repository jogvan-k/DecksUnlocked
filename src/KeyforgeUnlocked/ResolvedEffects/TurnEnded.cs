namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class TurnEnded : IResolvedEffect
  {
    bool Equals(TurnEnded other)
    {
      return true;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((TurnEnded) obj);
    }

    public override int GetHashCode()
    {
      return GetType().GetHashCode();
    }

    public override string ToString()
    {
      return $"Turn ended";
    }
  }
}