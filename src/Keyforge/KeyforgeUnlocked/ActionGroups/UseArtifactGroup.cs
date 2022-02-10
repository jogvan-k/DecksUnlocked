using System;
using System.Collections.Immutable;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Artifacts;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.ActionGroups
{
    public class UseArtifactGroup : ActionGroupBase<UseArtifactGroup>
    {
        public Artifact Artifact;
        readonly bool _allowOutOfHouseUse;

        public UseArtifactGroup(
            Artifact artifact,
            bool allowOutOfHouseUse = false)
        {
            Artifact = artifact;
            _allowOutOfHouseUse = allowOutOfHouseUse;
        }

        protected override IImmutableList<IAction> InitiateActions(ImmutableState origin)
        {
            var actions = ImmutableList<IAction>.Empty;

            if (!_allowOutOfHouseUse && Artifact.Card.House != origin.ActiveHouse)
                return actions;

            if (Artifact.IsReady && Artifact.ActionAbility != null)
                actions = actions.Add(new UseArtifact(origin, Artifact, _allowOutOfHouseUse)); // Add action

            return actions;
        }

        protected override bool Equals(UseArtifactGroup other)
        {
            return Artifact.Equals(other.Artifact) && _allowOutOfHouseUse == other._allowOutOfHouseUse;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Artifact, _allowOutOfHouseUse);
        }

        public override string ToString()
        {
            return "Use artifact";
        }
    }
}