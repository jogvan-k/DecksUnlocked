namespace KeyforgeUnlocked.Types
{
  /// <summary>
  /// Base class that provides a standard implementation of <see cref="Equals"/> and <see cref="GetHashCode"/>. 
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public abstract class Equatable<T>
  {
    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != GetType()) return false;
      return Equals((T) obj);
    }

    protected virtual bool Equals(T other)
    {
      return true;
    }


    public override int GetHashCode()
    {
      return GetType().GetHashCode();
    }
  }
}