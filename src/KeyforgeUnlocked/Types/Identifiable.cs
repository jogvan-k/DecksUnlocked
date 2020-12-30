namespace KeyforgeUnlocked.Types
{
  /// <summary>
  /// Lightweight implementation of <see cref="IIdentifiable"/> that only contains the Id and name.
  /// </summary>
  public class Identifiable : IIdentifiable
  {
    public string Id { get; }

    public string Name { get; }

    public Identifiable(IIdentifiable identifiable)
    {
      Id = identifiable.Id;
      Name = identifiable.Name;
    }

    public Identifiable(string id)
    {
      Id = id;
      Name = id;
    }

    public override bool Equals(object obj)
    {
      if (obj == null) return false;
      if (obj is IIdentifiable identifiable)
        return Id.Equals(identifiable.Id);
      return false;
    }

    public override int GetHashCode()
    {
      return Id.GetHashCode();
    }

    public override string ToString()
    {
      return Name;
    }
  }
}