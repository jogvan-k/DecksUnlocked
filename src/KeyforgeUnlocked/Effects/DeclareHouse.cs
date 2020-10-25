using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Effects
{
  public sealed class DeclareHouse : EffectBase<DeclareHouse>
  {
    protected override void ResolveImpl(MutableState state)
    {
      var Metadata = state.Metadata;
      if (Metadata == null)
        throw new NoMetadataException(state);

      var availableHouses = Metadata.Houses[state.PlayerTurn];
      state.ActionGroups.Add(new DeclareHouseGroup(availableHouses));
    }
  }
}