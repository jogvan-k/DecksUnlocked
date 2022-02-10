using KeyforgeUnlocked.Artifacts;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Effects
{
    public sealed class PlayArtifactCard : PlayCard<PlayArtifactCard>
    {
        public PlayArtifactCard(ICard card) : base(card)
        {
        }

        protected override void ResolveImpl(IMutableState state)
        {
            state.Artifacts[state.PlayerTurn].Add(new Artifact((IArtifactCard)Card));
            state.ResolvedEffects.Add(new ArtifactCardPlayed(Card));
            ResolvePlayEffects(state);
        }
    }
}