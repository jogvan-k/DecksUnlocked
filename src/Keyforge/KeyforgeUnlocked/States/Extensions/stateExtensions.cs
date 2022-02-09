using System;
using KeyforgeUnlocked.Artifacts;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.States.Extensions
{
  public static class stateExtensions
  {
    public static void Destroy(this IMutableState state, IIdentifiable id)
    {
      if(id is Creature)
        state.DestroyCreature(id);
      else if (id is Artifact)
        state.DestroyArtifact(id);
      else throw new Exception($"{id} is expected to be either of type Creature or of type Artifact");
    }
  }
}