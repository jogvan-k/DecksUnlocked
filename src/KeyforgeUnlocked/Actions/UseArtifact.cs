using KeyforgeUnlocked.Artifacts;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Actions
{
  public sealed class UseArtifact : BasicAction<UseArtifact>
  {
    public Artifact Artifact { get; }
    public bool AllowOutOfHouseUse { get; }
    
    public UseArtifact(ImmutableState origin, Artifact artifact, bool allowOutOfHouseUse) : base(origin)
    {
      Artifact = artifact;
      AllowOutOfHouseUse = allowOutOfHouseUse;
    }

    protected override void DoSpecificActionNoResolve(IMutableState state)
    {
      if (Artifact.ActionAbility == null)
        throw new NoCallbackException(state, Artifact);
      
      Artifact.ActionAbility(state, Artifact, state.PlayerTurn);
      var a = Artifact;
      a.IsReady = false;
      state.Artifacts[state.PlayerTurn].Remove(Artifact);
      state.Artifacts[state.PlayerTurn].Add(a);
      state.ResolvedEffects.Add(new ArtifactUsed(a));
    }
    
    
    internal override void Validate(IState state)
    {
      base.Validate(state);
      ValidateActiveHouse(state);
      ValidateCreatureIsReady(state);
    }

    void ValidateCreatureIsReady(IState state)
    {
      if (!Artifact.IsReady)
        throw new ArtifactNotReadyException(state, Artifact);
    }

    void ValidateActiveHouse(IState state)
    {
      var house = state.ActiveHouse;
      if (!AllowOutOfHouseUse && Artifact.Card.House != house)
        throw new NotFromActiveHouseException(state, Artifact.Card, house ?? House.Undefined);
    }
  }
}