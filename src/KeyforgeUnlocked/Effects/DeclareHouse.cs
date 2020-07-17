using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Effects
{
  public sealed class DeclareHouse : IEffect
  {
    public void Resolve(MutableState state)
    {
      var Metadata = state.Metadata;
      if (Metadata == null)
        throw new NoMetadataException(state);

      var availableHouses = Metadata.Houses[state.PlayerTurn];
      state.ActionGroups.Add(new DeclareHouseGroup(availableHouses));
    }

    bool Equals(DeclareHouse other)
    {
      return true;
    }

    public override bool Equals(object obj)
    {
      return ReferenceEquals(this, obj) || obj is DeclareHouse other && Equals(other);
    }

    public override int GetHashCode()
    {
      return 1;
    }
  }
}