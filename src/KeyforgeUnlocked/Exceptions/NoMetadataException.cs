using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Exceptions
{
  public sealed class NoMetadataException : KeyforgeUnlockedException
  {
    public NoMetadataException(IState state) : base(state)
    {
    }

    bool Equals(NoMetadataException other)
    {
      return base.Equals(other);
    }

    public override bool Equals(object? obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((NoMetadataException) obj);
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }
}