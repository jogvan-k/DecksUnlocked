using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Effects
{
  public sealed class DeclareHouse : EffectBase<DeclareHouse>
  {
    protected override void ResolveImpl(IMutableState state)
    {
      var metadata = state.Metadata;
      if (metadata == null)
        throw new NoMetadataException(state);

      var availableHouses = metadata.Houses[state.PlayerTurn];
      state.ActionGroups.Add(new DeclareHouseGroup(availableHouses));
    }
  }
}