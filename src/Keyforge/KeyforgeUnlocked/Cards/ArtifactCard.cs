using System;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards
{
    public abstract class ArtifactCard : Card, IArtifactCard
    {
        public Trait[] CardTraits { get; }
        public Callback? CardActionAbility { get; }

        public ArtifactCard(House house, Trait[]? traits, Pip[]? pips = null, Callback? playAbility = null,
            Callback? actionAbility = null) : base(house, pips, playAbility)
        {
            CardTraits = traits ?? Array.Empty<Trait>();
            CardActionAbility = actionAbility;
        }
    }
}