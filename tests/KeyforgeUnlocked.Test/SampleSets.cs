using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using UnlockedCore;

namespace KeyforgeUnlockedTest
{
    public class SampleSets
    {
        static readonly ICard[] _sampleDeck = Enumerable.Range(1, 36).Select(i => new SampleCreatureCard()).ToArray();
        static readonly ICard[] _sampleSet = Enumerable.Range(1, 6).Select(i => new SampleCreatureCard()).ToArray();

        public static Stack<ICard> SampleDeck => new Stack<ICard>(_sampleDeck);

        public static IMutableSet<ICard> SampleSet = new LazySet<ICard>(_sampleSet);

        public static IImmutableDictionary<Player, IMutableSet<ICard>> SampleHands =>
            new Dictionary<Player, IMutableSet<ICard>>
            {
                { Player.Player1, SampleSet },
                { Player.Player2, SampleSet }
            }.ToImmutableDictionary();
    }
}