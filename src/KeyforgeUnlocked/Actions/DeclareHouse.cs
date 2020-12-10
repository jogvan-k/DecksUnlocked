using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Actions
{
  public sealed class DeclareHouse : Action
  {
    public House House { get; }

    public DeclareHouse(ImmutableState origin, House house) : base(origin)
    {
      House = house;
    }

    internal override void Validate(IState state)
    {
      var metadata = state.Metadata;
      if(metadata == null)
        throw new NoMetadataException(state);
      if(!metadata.Houses[state.PlayerTurn].Contains(House))
        throw new DeclaredHouseNotAvailableException(state, House);
    }

    internal override void DoActionNoResolve(MutableState state)
    {
      state.ActiveHouse = House;
      state.ResolvedEffects.Add(new HouseDeclared(House));
    }

    public override string Identity()
    {
      return House.ToString();
    }

    bool Equals(DeclareHouse other)
    {
      return House == other.House;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((DeclareHouse) obj);
    }

    public override int GetHashCode()
    {
      return (int) House;
    }
  }
}