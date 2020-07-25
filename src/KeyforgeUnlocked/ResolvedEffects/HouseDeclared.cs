using KeyforgeUnlocked.Cards;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public class HouseDeclared : IResolvedEffect
  {
    public readonly House House;

    public HouseDeclared(House house)
    {
      House = house;
    }

    protected bool Equals(HouseDeclared other)
    {
      return House == other.House;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((HouseDeclared) obj);
    }

    public override int GetHashCode()
    {
      return (int) House;
    }
  }
}