using KeyforgeUnlocked.Types;

namespace KeyforgeUnlockedTest.Types
{
  public class Identifiable : IIdentifiable
  {
    public string Id { get; }
    public string Name { get; }

    public Identifiable(string id, string name = null)
    {
      Id = id;
      Name = name;
    }
  }
}