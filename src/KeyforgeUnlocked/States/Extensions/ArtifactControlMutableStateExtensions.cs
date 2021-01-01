using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.Types;
using static KeyforgeUnlocked.States.Extensions.ExtensionsUtil;

namespace KeyforgeUnlocked.States.Extensions
{
  public static class ArtifactControlMutableStateExtensions
  {
    public static void DestroyArtifact(
      this IMutableState state,
      IIdentifiable id)
    {
      if (!TryRemove(state.Artifacts, id, out var owningPlayer, out var artifact))
        throw new ArtifactNotPresentException(state, id);

      state.Discards[owningPlayer].Add(artifact.Card);
      state.ResolvedEffects.Add(new ArtifactDestroyed(artifact));
    }
  }
}